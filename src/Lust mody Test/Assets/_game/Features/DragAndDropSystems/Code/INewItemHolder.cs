namespace Features.DragAndDropSystems
{
	public interface INewItemHolder : IItemHolder
	{
		void Hold(string id);
		void Unhold();
		string Id { get; }
	}
}