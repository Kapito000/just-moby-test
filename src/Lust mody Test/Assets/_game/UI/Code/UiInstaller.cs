using UI.CubesMenus;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace UI
{
	public class UiInstaller : MonoInstaller
	{
		[SerializeField] MainMediator _mainMediator;
		[SerializeField] CubesListView _cubesListView;

		public override void InstallBindings()
		{
			BindMainMediator();
			BindCubesListView();
			BindCubesListViewFactory();
		}

		void BindMainMediator()
		{
			Assert.IsNotNull(_mainMediator);

			Container
				.BindInterfacesTo<MainMediator>()
				.FromInstance(_mainMediator)
				.AsSingle();
		}

		void BindCubesListViewFactory()
		{
			Container
				.BindInterfacesTo<CubesListViewFactory>()
				.AsSingle();
		}

		void BindCubesListView()
		{
			Assert.IsNotNull(_cubesListView);

			Container
				.BindInterfacesTo<CubesListView>()
				.FromInstance(_cubesListView)
				.AsSingle();
		}
	}
}