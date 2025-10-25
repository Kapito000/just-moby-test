using Features.DragAndDropSystems.ItemHolders;
using Features.DragAndDropSystems.ItemPlacers;
using UnityEngine;

namespace Features.DragAndDropSystems
{
	public sealed class DropProcessor : IDropProcessor
	{
		INewItemPlacer _newItemPlacer;
		ITowerItemDropProcessor _towerItemDropProcessor;

		Vector2 _screenPos;
		IDraggedItem _draggedItem;

		public DropProcessor(INewItemPlacer newItemPlacer, ITowerItemDropProcessor towerItemDropProcessor)
		{
			_newItemPlacer = newItemPlacer;
			_towerItemDropProcessor = towerItemDropProcessor;
		}

		public IDropProcessor SetScreenPos(Vector2 screenPos)
		{
			_screenPos = screenPos;
			return this;
		}

		public IDropProcessor SetDraggedItem(IDraggedItem draggedItem)
		{
			_draggedItem = draggedItem;
			return this;
		}

		public void Visit(INewItemHolder newItemHolder)
		{
			_newItemPlacer.Place(_screenPos, newItemHolder.Id, _draggedItem.Size);
		}

		public void Visit(ITowerItemHolder towerItemHolder)
		{
			_towerItemDropProcessor.Process(_screenPos, towerItemHolder.Item);
		}
	}
}