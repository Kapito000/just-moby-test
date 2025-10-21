using UnityEngine;

namespace Infrastructure.AssetManagements
{
	public interface IAssetProvider : IService
	{
		GameObject Load(string key);
	}
}