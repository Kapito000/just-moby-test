using Features.Items;

namespace UI.ItemsMenus
{
	public interface IItemListView
	{
		void UpdateList(IItemConfigDataProvider[] configs);
	}
}