using UnityEngine;

namespace Features.DragAndDropSystems
{
	public interface IDragSystem
	{
		void StarDrag(Sprite skin);
		void Stop();
		void Drag(Vector2 pos);
	}
}