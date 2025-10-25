using Features.Items;

namespace Features.DragAndDropSystems.ItemHolders
{
	public interface ITowerItemHolder : IItemHolder
	{
		IItem Item { get; }
		void Hold(IItem item);
		void Unhold();
	}
}