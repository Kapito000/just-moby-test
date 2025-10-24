using Features.Items;
using UI.ItemsMenus;
using UnityEngine;
using Zenject;

namespace UI
{
	public sealed class MainMediator : MonoBehaviour, IMainMediator
	{
		[Inject] IItemListView _itemListView;

		public void CubesListViewUpdate(IItemConfigDataProvider[] configs) => _itemListView.UpdateList(configs);
	}
}