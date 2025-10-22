using Features.Cubes;
using UI.CubesMenus;
using UnityEngine;
using Zenject;

namespace UI
{
	public sealed class MainMediator : MonoBehaviour, IMainMediator
	{
		[Inject] ICubesListView _cubesListView;

		public void CubesListViewUpdate(ICubeConfigDataProvider[] configs) => _cubesListView.UpdateList(configs);
	}
}