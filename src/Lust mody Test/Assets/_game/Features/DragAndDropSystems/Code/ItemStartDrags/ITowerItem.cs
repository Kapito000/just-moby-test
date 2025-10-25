using Features.Items;

namespace Features.DragAndDropSystems.ItemStartDrags
{
	public interface ITowerItem
	{
		string Id { get; }
		IItem Item { get; }
	}
}