using Features.Towers;

namespace Features.DragAndDropSystems
{
	public interface ITowerPlace : IItemPlace
	{
		void Place(ItemPlaceData placeData);
	}
}