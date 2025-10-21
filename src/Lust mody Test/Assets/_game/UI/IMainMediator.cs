using Features.Cubes;

namespace UI
{
	public interface IMainMediator
	{
		void CubesListViewUpdate(ICubeConfigDataProvider[] configs);
	}
}