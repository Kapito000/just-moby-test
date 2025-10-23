using Features.Cubes;
using Features.DragAndDropCubes;
using UnityEngine;

namespace Features.Towers
{
	public sealed class CubePlacer : MonoBehaviour, ICubePlacer
	{
		ICubePlacer _placer;

		public void Init(ICubePlacer placer)
		{
			_placer = placer;
		}

		public void Place(Vector2 pos, string cubeDataId)
		{
			_placer.Place(pos, cubeDataId);
		}

		public void Remove(GameCube cube)
		{
			throw new System.NotImplementedException();
		}
	}
}