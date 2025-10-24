using Features.Items;

namespace Features.DragAndDropSystems
{
	public sealed class NewItemHolder : INewItemHolder
	{
		IItem _item;
		IItemHolder _itemHolder = new ItemHolder();

		public bool IsHold => _itemHolder.IsHold;
		public IItem Item => _item;

		public void Hold(bool value) =>
			_itemHolder.Hold(value);

		public void Hold(IItem item)
		{
			_item = item;
			Hold(true);
		}

		public void Unhold()
		{
			Hold(false);
		}
	}
}