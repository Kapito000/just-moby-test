namespace Features.DragAndDropSystems
{
	public sealed class ItemHolder : IItemHolder
	{
		bool _isHold;

		public bool IsHold => _isHold;

		public void Hold(bool value)
		{
			_isHold = value;
		}

		public void Accept(IItemHolderVisitor visitor)
		{ }
	}
}