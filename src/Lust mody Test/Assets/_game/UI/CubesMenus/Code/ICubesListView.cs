using Features.Cubes;

namespace UI.CubesMenus
{
	public interface ICubesListView
	{
		void UpdateList(ICubeConfigDataProvider[] configs);
	}
}