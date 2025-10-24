namespace Features.DragAndDropSystems
{
	public sealed class NewItemHolder : INewItemHolder
	{
		readonly IItemHolder _itemHolder = new ItemHolder();
		
		string _id;

		public bool IsHold => _itemHolder.IsHold;
		public string Id => _id;

		public void Hold(bool value) =>
			_itemHolder.Hold(value);

		public void Hold(string id)
		{
			_id = id;
			Hold(true);
		}

		public void Unhold()
		{
			Hold(false);
		}
	}
}