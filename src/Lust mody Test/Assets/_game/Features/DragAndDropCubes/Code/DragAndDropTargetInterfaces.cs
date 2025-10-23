using Features.Cubes;
using UnityEngine;

namespace Features.DragAndDropCubes
{
	public interface IDragTarget
	{
		string CubeId { get; }
	}

	public interface IDropTarget
	{ }

	public interface ICubePlacer
	{
		void Place(Vector2 pos, string cubeDataId);
		void Remove(GameCube cube);
	}
}