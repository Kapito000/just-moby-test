using Features.DragAndDropSystems.ItemHolders;
using UnityEngine;

namespace Features.DragAndDropSystems
{
	public interface IDropProcessor : IItemHolderVisitor
	{
		IDropProcessor SetScreenPos(Vector2 screenPos);
		IDropProcessor SetDraggedItem(IDraggedItem draggedItem);
	}
}