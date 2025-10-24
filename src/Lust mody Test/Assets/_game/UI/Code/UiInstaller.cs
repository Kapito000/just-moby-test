using UI.ItemsMenus;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace UI
{
	public class UiInstaller : MonoInstaller
	{
		[SerializeField] MainMediator _mainMediator;
		[SerializeField] ItemsListView itemsListView;

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
				.BindInterfacesTo<ItemsListViewFactory>()
				.AsSingle();
		}

		void BindCubesListView()
		{
			Assert.IsNotNull(itemsListView);

			Container
				.BindInterfacesTo<ItemsListView>()
				.FromInstance(itemsListView)
				.AsSingle();
		}
	}
}