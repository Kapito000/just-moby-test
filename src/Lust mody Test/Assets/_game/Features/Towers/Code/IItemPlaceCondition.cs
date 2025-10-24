namespace Features.Towers
{
	public interface IItemPlaceCondition
	{
		bool CanPlace(ItemPlaceData placeData);
	}
}