namespace Features.DragAndDropSystems.ItemHolders
{
	public interface IItemHolderVisitor
	{
		public void Visit(INewItemHolder newItemHolder);
		public void Visit(ITowerItemHolder towerItemHolder);
	}
}