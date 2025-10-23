using Constants;
using Infrastructure.AssetManagements;
using UnityEngine;
using Zenject;

namespace Features.Cubes
{
	public sealed class GameCubeFactory : IGameCubeFactory
	{
		[Inject] IInstantiator _instantiator;
		[Inject] IAssetProvider _assetProvider;
		[Inject] ICubesDataCollectionProvider _cubesDataProvider;

		public IGameCube Create(Vector2 pos, string cubeId)
		{
			var prefab = _assetProvider.Load(AssetKeys.GameCube);
			var obj = _instantiator.InstantiatePrefab(prefab, pos, Quaternion.identity, null);

			if (false == obj.TryGetComponent<GameCube>(out var gameCube) ||
			    false == _cubesDataProvider.TryGetConfig(cubeId, out var config))
			{
				Debug.LogError("Failed to create game cube.");
				return gameCube;
			}

			gameCube.DataId = config.Id;
			gameCube.RefreshSkin(config.Sprite);

			return gameCube;
		}

		public IGameCube Create()
		{
			var prefab = _assetProvider.Load(AssetKeys.GameCube);
			var obj = _instantiator.InstantiatePrefab(prefab, null);

			if (false == obj.TryGetComponent<GameCube>(out var gameCube))
				Debug.LogError("Failed to create game cube.");

			return gameCube;
		}
	}
}