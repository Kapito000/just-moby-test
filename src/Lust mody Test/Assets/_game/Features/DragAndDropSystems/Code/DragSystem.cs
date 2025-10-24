using UnityEngine;
using Zenject;

namespace Features.DragAndDropSystems
{
	public sealed class DragSystem : MonoBehaviour, IDragSystem
	{
		[Inject] IDraggedItem _draggedItem;

		public void StarDrag(Sprite skin)
		{
			_draggedItem.SetSkin(skin);
			_draggedItem.Show();
		}

		public void Stop()
		{
			_draggedItem.Hide();
		}

		public void Drag(Vector2 pos)
		{
			_draggedItem.Move(pos);
		}
	}
}