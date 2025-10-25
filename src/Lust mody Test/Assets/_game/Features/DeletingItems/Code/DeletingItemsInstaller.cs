using Zenject;

namespace Features.DeletingItems
{
	public sealed class DeletingItemsInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			DestroyItemViewSystem();
		}

		void DestroyItemViewSystem()
		{
			Container
				.BindInterfacesTo<DestroyItemViewSystem>()
				.AsSingle();
		}
	}
}