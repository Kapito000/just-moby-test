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
		[Inject] ICubePlacer _cubePlacer;
		[Inject] IGameCubeFactory _gameCubeFactory;
		[Inject] ICubesDataCollectionProvider _cubesDataProvider;

		GameCube _cube;
		GameCube _towerCube;
		UiRayCaster _uiRayCaster;

		Func<GameCube, bool>[] _placeConditions;

		public Vector2 PointerPosition { get; set; }

		Camera Camera => _sceneData.Camera;

		[Inject]
		void Construct(ISceneData sceneData)
		{
			_uiRayCaster = new UiRayCaster(
				sceneData.EventSystem, sceneData.GraphicRaycaster);
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
			if (TryDragUiCube(screenPos))
				return;

			if (TryDragCube(screenPos))
				return;
		}

		public void TryDrop(Vector2 screenPos)
		{
			if (TryDropToTower(screenPos))
				return;

			if (TryRemoveCube(screenPos))
				return;
		}

		public bool CubeInCameraView(GameCube cube)
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
		}

		bool TryRemoveCube(Vector2 screenPos)
		{
			if (_towerCube == null)
				return false;

			var origin = _sceneData.Camera.ScreenToWorldPoint(screenPos);
			var hit = Physics2D.Raycast(origin, Vector2.zero);

			if (hit.collider == null)
				return false;

			if (hit.collider.CompareTag(Constants.Tag.RemoveCubeArea) == false)
			{
				_towerCube.Unselect();
				UnholdCube();
				return false;
			}

			_cubePlacer.Remove(_towerCube);
			return true;
		}

		bool TryDropToTower(Vector2 screenPos)
		{
			if (_isHold == false)
				return false;
			
			if (TryPlaceCube() == false)
			{
				UnholdCube();
				return false;
			}

			var pos = Camera.ScreenToWorldPoint(screenPos);
			var placer = PhysicsRayCaster.CastRay<ICubePlacer>(pos);
			if (placer == null)
			{
				UnholdCube();
				return false;
			}

			placer.Place(pos, _cube.DataId);

			UnholdCube();

			return true;
		}

		bool TryDragCube(Vector2 screenPos)
		{
			var pos = Camera.ScreenToWorldPoint(screenPos);
			var gameCube = PhysicsRayCaster.CastRay<GameCube>(pos);
			if (gameCube == null)
				return false;

			gameCube.Select();
			HoldCube(gameCube);

			return true;
		}

		bool TryDragUiCube(Vector2 screenPos)
		{
			var dragTarget = _uiRayCaster.CastRay<IDragTarget>(screenPos);
			if (dragTarget == null)
				return false;

			if (TryShowCube(screenPos, dragTarget) == false)
				return false;

			HoldCube();

			return true;
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
			_cube.EnableCollider(false);

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

		void HoldCube(GameCube cube)
		{
			_isHold = true;
			_cube.Enable(true);
			_towerCube = cube;
		}

		void UnholdCube()
		{
			_isHold = false;
			_cube.Enable(false);
			_towerCube = null;
		}

		Func<GameCube, bool>[] ConstructPlaceConditions()
		{
			return new Func<GameCube, bool>[]
			{
				CubeInCameraView,
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
						var dropTarget = PhysicsRayCaster.CastRay<IDropTarget>(point);
						if (dropTarget == null)
							return false;
					}

					return true;
				},
			};
		}
	}
}