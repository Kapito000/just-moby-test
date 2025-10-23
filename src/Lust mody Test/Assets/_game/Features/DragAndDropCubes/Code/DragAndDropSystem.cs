using System;
using Extensions;
using Features.Common;
using Features.Cubes;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.DragAndDropCubes
{
	public sealed class DragAndDropSystem : MonoBehaviour, IDragAndDropSystem
	{
		[ReadOnly]
		[SerializeField] bool _isHold;

		[Inject] ISceneData _sceneData;
		[Inject] IGameCubeFactory _gameCubeFactory;

		IGameCube _capturedCube;
		UiRayCaster _uiRayCaster;
		PhysicsRayCaster _physicsRaycaster;

		Func<IGameCube, bool>[] _placeConditions;

		public Vector2 PointerPosition { get; set; }

		Camera Camera => _sceneData.Camera;

		[Inject]
		void Construct(ISceneData sceneData)
		{
			_uiRayCaster = new UiRayCaster(
				sceneData.EventSystem, sceneData.GraphicRaycaster);

			_physicsRaycaster = new PhysicsRayCaster(sceneData.Camera);
		}

		void Awake()
		{
			Observable.EveryUpdate()
				.Where(_ => _isHold)
				.Subscribe(_ => { MoveCube(); })
				.AddTo(this);

			_placeConditions = ConstructPlaceConditions();
		}

		public void TryStartDrag(Vector2 screenPos)
		{
			var dragTarget = _uiRayCaster.CastRay<IDragTarget>(screenPos);
			if (dragTarget == null)
				return;

			var id = dragTarget.CubeId;
			var pos = _sceneData.Camera.ScreenToWorldPoint(screenPos).AsVector2();
			var gameCube = _gameCubeFactory.Create(pos, id);

			HoldCube(gameCube);
		}

		public void TryDrop(Vector2 screenPos)
		{
			foreach (var condition in _placeConditions)
			{
				if (condition.Invoke(_capturedCube) == false)
				{
					_capturedCube.Destroy();
					UnholdCube();
					return;
				}
			}

			var dropTarget = _physicsRaycaster.CastRay<IDropTarget>(screenPos);
			dropTarget.Place(_capturedCube);

			UnholdCube();
		}

		void MoveCube()
		{
			var pos = _sceneData.Camera.ScreenToWorldPoint(PointerPosition).AsVector2();
			_capturedCube.SetPosition(pos);
		}

		void HoldCube(IGameCube cube)
		{
			_isHold = true;
			_capturedCube = cube;
		}

		void UnholdCube()
		{
			_isHold = false;
			_capturedCube = null;
		}

		Func<IGameCube, bool>[] ConstructPlaceConditions()
		{
			return new Func<IGameCube, bool>[]
			{
				cube =>
				{
					var width = _sceneData.Camera.pixelWidth;
					var height = _sceneData.Camera.pixelHeight;

					foreach (var point in cube.SizePoints())
					{
						var screenPos = Camera.WorldToScreenPoint(point);
						if (screenPos.x < 0 || screenPos.y > height ||
						    screenPos.y < 0 || screenPos.x > width)
						{
							return false;
						}
					}

					return true;
				},
				cube =>
				{
					foreach (var point in cube.SizePoints())
					{
						var screenPos = Camera.WorldToScreenPoint(point);
						var dragTarget = _uiRayCaster.CastRay<IDragTarget>(screenPos);
						if (dragTarget != null)
							return false;
					}

					return true;
				},
				cube =>
				{
					foreach (var point in cube.SizePoints())
					{
						var screenPos = Camera.WorldToScreenPoint(point);
						var dropTarget = _physicsRaycaster.CastRay<IDropTarget>(screenPos);
						if (dropTarget == null)
							return false;
					}

					return true;
				},
			};
		}
	}
}