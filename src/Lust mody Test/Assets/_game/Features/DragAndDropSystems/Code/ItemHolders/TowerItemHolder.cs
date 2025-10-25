using Features.Items;

namespace Features.DragAndDropSystems.ItemHolders
{
	public sealed class TowerItemHolder : ITowerItemHolder
	{
		readonly IItemHolder _itemHolder = new ItemHolder();

		IItem _item;

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

		public void Accept(IItemHolderVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}