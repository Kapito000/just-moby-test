using Features.Cubes.StaticData;
using UnityEngine;

namespace Features.Cubes
{
	public sealed class CubeConfigDataProvider : ICubeConfigDataProvider
	{
		readonly CubeConfig _data;

		public CubeConfigDataProvider(CubeConfig config)
		{
			_data = config;
		}

		public string Id => _data.Id;
		public Sprite Sprite => _data.Sprite;
	}
}