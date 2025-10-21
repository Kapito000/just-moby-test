using UnityEngine;
using Zenject;

namespace UI.CubesMenus
{
	public interface ICubesListViewFactory : IFactory
	{
		CubeListItemView CreateItem(Transform parent);
	}
}