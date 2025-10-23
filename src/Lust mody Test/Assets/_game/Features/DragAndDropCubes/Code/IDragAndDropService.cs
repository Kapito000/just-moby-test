using Features.Cubes;
using Infrastructure;
using UnityEngine;

namespace Features.DragAndDropCubes
{
	public interface IDragAndDropSystem : ISystem
	{
		void TryStartDrag(Vector2 screenPos);
		void TryDrop(Vector2 screenPos);
		Vector2 PointerPosition { get; set; }
		bool CubeInCameraView(GameCube cube);
	}
}