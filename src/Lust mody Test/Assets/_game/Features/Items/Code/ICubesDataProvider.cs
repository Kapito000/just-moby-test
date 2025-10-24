using System.Collections.Generic;

namespace Features.Items
{
	public interface IItemsDataCollectionProvider
	{
		bool Contains(string key);
		IItemConfigDataProvider GetConfig(string key);
		bool TryGetConfig(string key, out IItemConfigDataProvider value);
		IEnumerable<string> Ids();
		IEnumerable<IItemConfigDataProvider> Configs();
	}
}