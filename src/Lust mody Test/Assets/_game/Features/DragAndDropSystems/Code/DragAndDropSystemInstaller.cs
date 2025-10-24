using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Features.DragAndDropSystems
{
	public sealed class DragAndDropSystemInstaller : MonoInstaller
	{
		[SerializeField] DragSystem _dragSystem;
		[SerializeField] DraggedItem _draggedItem;
		[SerializeField] NewItemHolder _newItemHolder;
		[SerializeField] DragAndDropSystem _dragAndDropSystem;

		public override void InstallBindings()
		{
			BindDragSystem();
			BindItemPlacers();
			BindDraggedItem();
			BindItemHolders();
			BindNewItemDrag();
			BindDragAndDropService();
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
			Assert.IsNotNull(_newItemHolder);

			Container
				.BindInterfacesTo<ItemHolder>()
				.FromInstance(_newItemHolder)
				.AsSingle();
		}

		void BindNewItemDrag()
		{
			Container
				.BindInterfacesTo<NewItemDrag>()
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