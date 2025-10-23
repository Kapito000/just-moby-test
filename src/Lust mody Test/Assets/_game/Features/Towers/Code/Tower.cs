using System.Collections.Generic;
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

		public void Place(string cubeDataId, Vector2 pos)
		{
			if (IsTowerEmpty() == false)
			{
				Debug.Log("Place cube to up of the tower.");
				return;
			}

			var newCube = _gameCubeFactory.Create(pos, cubeDataId);
			CreateNewPlacement(newCube);
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