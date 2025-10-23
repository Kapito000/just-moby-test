using Features.Cubes;
using UnityEngine;

namespace Features.DragAndDropCubes
{
	public interface IDragTarget
	{
		string CubeId { get; }
	}

	public interface IDropTarget
	{
		void Place(string cubeDataId, Vector2 pos);
	}
}