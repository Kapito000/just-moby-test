using Constants;
using Infrastructure.AssetManagements;
using UnityEngine;
using Zenject;

namespace UI.ItemsMenus
{
	public sealed class ItemsListViewFactory : IItemsListViewFactory
	{
		[Inject] IInstantiator _instantiator;
		[Inject] IAssetProvider _assetProvider;

		public ItemView CreateItem(Transform parent)
		{
			var itemPrefab = _assetProvider.Load(AssetKeys.CubeListItemView);
			var itemObj = _instantiator.InstantiatePrefab(itemPrefab, parent);

			if (false == itemObj.TryGetComponent<ItemView>(out var itemView))
				Debug.LogError("Failed to create item view");

			return itemView;
		}
	}
}