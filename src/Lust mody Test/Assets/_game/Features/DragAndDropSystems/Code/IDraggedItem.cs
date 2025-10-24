using Features.Items;
using UnityEngine;

namespace Features.DragAndDropSystems
{
	public interface IDraggedItem
	{
		void Move(Vector2 pos);
		void SetSkin(Sprite skin);
		void Show();
		void Hide();
		IItemSize Size { get; }
	}
}