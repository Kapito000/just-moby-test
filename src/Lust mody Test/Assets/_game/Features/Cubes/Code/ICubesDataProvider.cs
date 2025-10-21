using System.Collections.Generic;

namespace Features.Cubes
{
	public interface ICubesDataCollectionProvider
	{
		bool Contains(string key);
		ICubeConfigDataProvider GetConfig(string key);
		bool TryGetConfig(string key, out ICubeConfigDataProvider value);
		IEnumerable<string> Ids();
		IEnumerable<ICubeConfigDataProvider> Configs();
	}
}