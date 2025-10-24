using System.Collections.Generic;
using Features.Items.StaticData;
using UnityEngine;

namespace Features.Items
{
	public sealed class ItemsDataCollectionProvider : IItemsDataCollectionProvider
	{
		Dictionary<string, IItemConfigDataProvider> _configurations = new();

		public ItemsDataCollectionProvider(CubesConfigsCollection data)
		{
			var configs = data.Configs;
			foreach (var conf in configs)
			{
				var key = conf.Id;
				if (_configurations.ContainsKey(key))
				{
					Debug.LogError("Duplicate character with ID: " + conf.Id);
					continue;
				}

				var confProvider = new ItemConfigDataProvider(conf);

				_configurations.Add(key, confProvider);
			}
		}

		public bool Contains(string key) =>
			_configurations.ContainsKey(key);

		public IItemConfigDataProvider GetConfig(string key) =>
			_configurations[key];

		public bool TryGetConfig(string key, out IItemConfigDataProvider value) =>
			_configurations.TryGetValue(key, out value);

		public IEnumerable<string> Ids() => _configurations.Keys;

		public IEnumerable<IItemConfigDataProvider> Configs() =>
			_configurations.Values;
	}
}