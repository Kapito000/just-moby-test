using Features.Cubes;
using Infrastructure;

namespace UI
{
	public interface IMainMediator : ISystem
	{
		void CubesListViewUpdate(ICubeConfigDataProvider[] configs);
	}
}