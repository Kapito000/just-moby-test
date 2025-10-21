using UnityEngine;
using Zenject;
using Menu = Constants.CreateAssetMenu;

namespace Infrastructure.AssetManagements
{
	[CreateAssetMenu(menuName = Menu.Installers + nameof(AssetProviderInstaller))]
	public class AssetProviderInstaller : ScriptableObjectInstaller
	{
		public override void InstallBindings()
		{
			BindAssetProvider();
		}

		void BindAssetProvider()
		{
			Container
				.BindInterfacesTo<AssetProvider>()
				.AsSingle();
		}
	}
}