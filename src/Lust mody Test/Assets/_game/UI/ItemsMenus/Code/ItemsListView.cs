using System.Collections.Generic;
using Features.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace UI.ItemsMenus
{
	public sealed class ItemsListView : MonoBehaviour, IItemListView
	{
		[SerializeField] Transform _itemsParent;
		[ReadOnly]
		[SerializeField] List<CubeListItemView> _items;

		[Inject] IItemsListViewFactory _factory;

		void Awake()
		{
			Assert.IsNotNull(_itemsParent);
		}

		public void UpdateList(IItemConfigDataProvider[] configs)
		{
			AdjustItemsCount(configs.Length);
			AdjustItemsViews(configs);
		}

		void AdjustItemsViews(IItemConfigDataProvider[] characters)
		{
			for (var i = 0; i < characters.Length; i++)
			{
				var item = _items[i];
				var data = characters[i];
				item.UpdateView(data);
				item.Show();
			}
		}

		void AdjustItemsCount(int targetCount)
		{
			var dif = _items.Count - targetCount;
			if (dif < 0)
			{
				for (int i = 0; i < -dif; i++)
				{
					CreateItem();
				}
			}
			else if (dif > 0)
			{
				for (int i = _items.Count - 1; dif != 0; i--, dif--)
				{
					_items[i].Hide();
				}
			}
		}

		void CreateItem()
		{
			var item = _factory.CreateItem(_itemsParent);
			_items.Add(item);
		}
	}
}