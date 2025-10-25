namespace Features.DragAndDropSystems.ItemHolders
{
	public sealed class TowerItemHolder : ITowerItemHolder
	{
		readonly IItemHolder _itemHolder = new ItemHolder();


		public bool IsHold => _itemHolder.IsHold;

		public void Hold(bool value) =>
			_itemHolder.Hold(value);

		public void Hold()
		{
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