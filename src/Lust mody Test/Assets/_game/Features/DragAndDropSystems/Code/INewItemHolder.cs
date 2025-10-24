using Features.Items;

namespace Features.DragAndDropSystems
{
	public interface INewItemHolder : IItemHolder
	{
		IItem Item { get; }
		void Hold(IItem item);
		void Unhold();
	}
}