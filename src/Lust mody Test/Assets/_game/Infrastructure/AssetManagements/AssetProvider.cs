using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.AssetManagements
{
	public sealed class AssetProvider : IAssetProvider
	{
		readonly Dictionary<string, GameObject> _loaded = new();

		public GameObject Load(string key)
		{
			if (_loaded.TryGetValue(key, out var result))
				return result;

			result = Resources.Load<GameObject>(key);
			_loaded.Add(key, result);
			
			return result;
		}
	}
}