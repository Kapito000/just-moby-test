using Constants;
using Infrastructure.AssetManagements;
using UnityEngine;
using Zenject;

namespace Features.Items
{
	public sealed class ItemFactory : IItemFactory
	{
		[Inject] IInstantiator _instantiator;
		[Inject] IAssetProvider _assetProvider;
		[Inject] IItemsDataCollectionProvider _itemsDataProvider;

		public Item Create(Vector2 pos, string cubeId)
		{
			var prefab = _assetProvider.Load(AssetKeys.GameCube);
			var obj = _instantiator.InstantiatePrefab(prefab, pos, Quaternion.identity, null);

			if (false == obj.TryGetComponent<Item>(out var item) ||
			    false == _itemsDataProvider.TryGetConfig(cubeId, out var config))
			{
				Debug.LogError("Failed to create game cube.");
				return item;
			}

			item.Id = config.Id;
			item.Skin = config.Sprite;

			return item;
		}
	}
}