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
		[Inject] ICubesDataCollectionProvider _cubesDataProvider;

		IGameCube _cube;
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

			_cube = _gameCubeFactory.Create();
			_cube.Enable(false);
		}

		public void TryStartDrag(Vector2 screenPos)
		{
			var dragTarget = _uiRayCaster.CastRay<IDragTarget>(screenPos);
			if (dragTarget == null)
				return;

			if (TryShowCube(screenPos, dragTarget) == false)
				return;

			HoldCube();
		}

		public void TryDrop(Vector2 screenPos)
		{
			if (TryPlaceCube() == false)
			{
				UnholdCube();
				return;
			}

			var pos = Camera.ScreenToWorldPoint(screenPos);
			var dropTarget = _physicsRaycaster.CastRay<IDropTarget>(screenPos);

			dropTarget.Place(_cube.DataId, pos);

			UnholdCube();
		}

		bool TryPlaceCube()
		{
			foreach (var condition in _placeConditions)
				if (condition.Invoke(_cube) == false)
					return false;

			return true;
		}

		bool TryShowCube(Vector2 screenPos, IDragTarget dragTarget)
		{
			var cubeDataId = dragTarget.CubeId;
			var pos = _sceneData.Camera.ScreenToWorldPoint(screenPos).AsVector2();
			if (false == _cubesDataProvider.TryGetConfig(cubeDataId, out var config))
			{
				Debug.Log($"Failed to find config for \"{cubeDataId}\".");
				return false;
			}

			_cube.DataId = cubeDataId;
			_cube.RefreshSkin(config.Sprite);
			_cube.Position = pos;
			_cube.Enable(true);

			return true;
		}

		void MoveCube()
		{
			var pos = _sceneData.Camera.ScreenToWorldPoint(PointerPosition).AsVector2();
			_cube.Position = pos;
		}

		void HoldCube()
		{
			_isHold = true;
			_cube.Enable(true);
		}

		void UnholdCube()
		{
			_isHold = false;
			_cube.Enable(false);
			_cube.Enable(false);
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