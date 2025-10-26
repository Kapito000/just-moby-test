using UnityEngine;
using Zenject;

namespace UI.ItemsMenus
{
	public interface IItemsListViewFactory : IFactory
	{
		ItemView CreateItem(Transform parent);
	}
}