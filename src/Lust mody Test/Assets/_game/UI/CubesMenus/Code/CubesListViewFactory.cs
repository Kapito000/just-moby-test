using Constants;
using Infrastructure.AssetManagements;
using UnityEngine;
using Zenject;

namespace UI.CubesMenus
{
	public sealed class CubesListViewFactory : ICubesListViewFactory
	{
		[Inject] IInstantiator _instantiator;
		[Inject] IAssetProvider _assetProvider;

		public CubeListItemView CreateItem(Transform parent)
		{
			var itemPrefab = _assetProvider.Load(AssetKeys.CubeListItemView);
			var itemObj = _instantiator.InstantiatePrefab(itemPrefab, parent);

			if (itemObj.TryGetComponent<CubeListItemView>(out var itemView) == false)
				Debug.LogError("Failed to create item view");

			return itemView;
		}
	}
}