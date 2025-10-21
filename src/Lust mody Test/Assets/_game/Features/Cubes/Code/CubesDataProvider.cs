using System.Collections.Generic;
using Features.Cubes.StaticData;
using UnityEngine;

namespace Features.Cubes
{
	public sealed class CubesDataCollectionProvider : ICubesDataCollectionProvider
	{
		Dictionary<string, ICubeConfigDataProvider> _configurations = new();

		public CubesDataCollectionProvider(CubesConfigsCollection data)
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

				var confProvider = new CubeConfigDataProvider(conf);

				_configurations.Add(key, confProvider);
			}
		}

		public bool Contains(string key) =>
			_configurations.ContainsKey(key);

		public ICubeConfigDataProvider GetConfig(string key) =>
			_configurations[key];

		public bool TryGetConfig(string key, out ICubeConfigDataProvider value) =>
			_configurations.TryGetValue(key, out value);

		public IEnumerable<string> Ids() => _configurations.Keys;

		public IEnumerable<ICubeConfigDataProvider> Configs() =>
			_configurations.Values;
	}
}