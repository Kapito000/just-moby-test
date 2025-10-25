using Features.DragAndDropSystems.ItemHolders;
using Features.DragAndDropSystems.ItemPlacers;
using Features.DragAndDropSystems.ItemStartDrags;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Features.DragAndDropSystems
{
	public sealed class DragAndDropSystemInstaller : MonoInstaller
	{
		[SerializeField] DragSystem _dragSystem;
		[SerializeField] DraggedItem _draggedItem;
		[SerializeField] DragAndDropSystem _dragAndDropSystem;

		public override void InstallBindings()
		{
			BindDragSystem();
			BindItemPlacers();
			BindDraggedItem();
			BindItemHolders();
			BindNewItemDrag();
			BindTowerItemDrag();
			BindNewItemHolder();
			BindDropProcessor();
			BindBaseItemPlacer();
			BindTowerItemHolder();
			BindDragAndDropService();
			BindTowerItemDropProcessor();
		}

		void BindDropProcessor()
		{
			Container
				.BindInterfacesTo<DropProcessor>()
				.AsSingle();
		}

		void BindTowerItemDropProcessor()
		{
			Container
				.BindInterfacesTo<TowerItemDropProcessor>()
				.AsSingle();
		}

		void BindBaseItemPlacer()
		{
			Container
				.Bind<BaseItemPlacer>()
				.AsTransient();
		}

		void BindTowerItemHolder()
		{
			Container
				.BindInterfacesTo<TowerItemHolder>()
				.AsSingle();
		}

		void BindNewItemHolder()
		{
			Container
				.BindInterfacesTo<NewItemHolder>()
				.AsSingle();
		}

		void BindItemPlacers()
		{
			Container
				.BindInterfacesTo<NewItemPlacer>()
				.AsSingle();
		}

		void BindDraggedItem()
		{
			Assert.IsNotNull(_draggedItem);

			Container
				.BindInterfacesTo<DraggedItem>()
				.FromInstance(_draggedItem)
				.AsSingle();
		}

		void BindDragSystem()
		{
			Assert.IsNotNull(_dragSystem);

			Container
				.BindInterfacesTo<DragSystem>()
				.FromInstance(_dragSystem)
				.AsSingle();
		}

		void BindItemHolders()
		{
			Container
				.BindInterfacesTo<ItemHolder>()
				.AsSingle();
		}

		void BindNewItemDrag()
		{
			Container
				.BindInterfacesTo<NewItemDrag>()
				.AsSingle();
		}

		void BindTowerItemDrag()
		{
			Container
				.BindInterfacesTo<TowerItemDrag>()
				.AsSingle();
		}

		void BindDragAndDropService()
		{
			Assert.IsNotNull(_dragAndDropSystem);

			Container
				.BindInterfacesTo<DragAndDropSystem>()
				.FromInstance(_dragAndDropSystem)
				.AsSingle();
		}
	}
}