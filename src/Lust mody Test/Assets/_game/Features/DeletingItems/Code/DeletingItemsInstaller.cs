using Constants;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Features.DeletingItems
{
	public sealed class DeletingItemsInstaller : MonoInstaller
	{
		[SerializeField] Transform _removeArea;
		
		public override void InstallBindings()
		{
			BindItemDeletingSystem();
			BindRemoveItemAreaTransform();
		}

		void BindRemoveItemAreaTransform()
		{
			Assert.IsNotNull(_removeArea);
			
			Container
				.Bind<Transform>()
				.WithId(DiKey.RemoveItemArea)
				.FromInstance(_removeArea);
		}

		void BindItemDeletingSystem()
		{
			Container
				.BindInterfacesTo<ItemDeletingSystem>()
				.AsSingle();
		}
	}
}