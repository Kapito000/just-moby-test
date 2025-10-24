using UnityEngine;
using Zenject;

namespace UI.ItemsMenus
{
	public interface IItemsListViewFactory : IFactory
	{
		CubeListItemView CreateItem(Transform parent);
	}
}