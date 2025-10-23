using System.Collections.Generic;
using Features.Common;
using Features.Cubes;
using Features.DragAndDropCubes;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Features.Towers
{
	public sealed class Tower : MonoBehaviour, IDropTarget
	{
		[ReadOnly]
		[SerializeField] List<CubePlacement> _placements;

		[Inject] IGameCubeFactory _gameCubeFactory;

		public void Place(Vector2 pos, string cubeDataId)
		{
			if (IsTowerEmpty() == false)
			{
				Debug.Log("Place cube to up of the tower.");

				var gameCube = PhysicsRayCaster.CastRay<GameCube>(pos);

				if (gameCube == null)
					return;

				var newPos = Vector2.one;
				var cube = CreateCube(newPos, cubeDataId);
				CreateNewPlacement(cube);

				return;
			}

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
			var placement = new CubePlacement()
			{
				Cube = cube,
			};

			_placements.Add(placement);
		}

		bool IsTowerEmpty() =>
			_placements.Count == 0;
	}
}