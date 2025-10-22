using Infrastructure;
using UnityEngine;

namespace Features.DragAndDropServices
{
	public interface IDragAndDropSystem : ISystem
	{
		void TryStartDrag(Vector2 screenPos);
		void TryDrop(Vector2 screenPos);
		Vector2 PointerPosition { get; set; }
	}
}