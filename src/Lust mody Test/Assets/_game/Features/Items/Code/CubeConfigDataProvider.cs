using Features.Items.StaticData;
using UnityEngine;

namespace Features.Items
{
	public sealed class ItemConfigDataProvider : IItemConfigDataProvider
	{
		readonly CubeConfig _data;

		public ItemConfigDataProvider(CubeConfig config)
		{
			_data = config;
		}

		public string Id => _data.Id;
		public Sprite Sprite => _data.Sprite;
	}
}