namespace Features.DragAndDropSystems.ItemHolders
{
	public interface INewItemHolder : IItemHolder
	{
		string Id { get; }
		void Hold(string id);
		void Unhold();
	}
}