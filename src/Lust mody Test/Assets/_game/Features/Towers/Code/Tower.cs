using System.Collections.Generic;
using Features.Common;
using Features.Cubes;
using Features.DragAndDropCubes;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Features.Towers
{
	public sealed class Tower : MonoBehaviour, IDropTarget, ICubePlacer
	{
		[SerializeField] float _verticalOffset = .5f;
		[SerializeField] float _horizontalOffset = .5f;

		[ReadOnly]
		[SerializeField] List<CubePlacement> _placements;

		[Inject] IGameCubeFactory _gameCubeFactory;
		[Inject] IDragAndDropSystem _dragAndDropSystem;

		public void Place(Vector2 pos, string cubeDataId)
		{
			if (IsTowerEmpty())
			{
				PlaceFirestCube(pos, cubeDataId);
				return;
			}

			PlaceNextCube(pos, cubeDataId);
		}

		public void Remove(GameCube cube)
		{
			int removesIndex = -1;
			for (var i = 0; i < _placements.Count; i++)
			{
				var placement = _placements[i];
				if (placement.Cube == cube)
				{
					removesIndex = i;
					break;
				}
			}

			if (removesIndex < 0)
				return;

			var removesPlacement = _placements[removesIndex];
			for (int i = removesIndex + 1; i < _placements.Count; i++)
			{
				var placement = _placements[i];
				placement.Pos -= new Vector2(0, _verticalOffset);
				placement.Cube.transform.position = placement.Pos;
			}

			_placements.RemoveAt(removesIndex);
		}

		void PlaceNextCube(Vector2 pos, string cubeDataId)
		{
			var gameCube = PhysicsRayCaster.CastRay<GameCube>(pos);
			var newPos = NextCubePosition();

			if (gameCube == null)
				return;

			var cube = CreateCube(newPos, cubeDataId);
			cube.Enable(false);

			if (_dragAndDropSystem.CubeInCameraView(cube) == false)
			{
				cube.Destroy();
				return;
			}

			CreateNewPlacement(cube);
			cube.Enable(true);
		}

		Vector2 NextCubePosition()
		{
			var placement = _placements[^1];
			var x = Random.Range(-_horizontalOffset, _horizontalOffset);
			var y = _verticalOffset;
			var pos = placement.Cube.transform.position + new Vector3(x, y, 0);
			return pos;
		}

		void PlaceFirestCube(Vector2 pos, string cubeDataId)
		{
			var newCube = CreateCube(pos, cubeDataId);
			CreateNewPlacement(newCube);
		}

		GameCube CreateCube(Vector2 newPos, string cubeDataId)
		{
			var cube = _gameCubeFactory.Create(newPos, cubeDataId);
			return cube;
		}

		void CreateNewPlacement(GameCube cube)
		{
			var cubePlacer = cube.gameObject.AddComponent<CubePlacer>();
			cubePlacer.Init(this);

			var placement = new CubePlacement()
			{
				Pos = cube.transform.position,
				Cube = cube,
			};

			_placements.Add(placement);
		}

		bool IsTowerEmpty() =>
			_placements.Count == 0;
	}
}