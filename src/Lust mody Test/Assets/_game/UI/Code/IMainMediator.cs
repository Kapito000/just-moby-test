using Features.Items;
using Infrastructure;

namespace UI
{
	public interface IMainMediator : ISystem
	{
		void CubesListViewUpdate(IItemConfigDataProvider[] configs);
	}
}