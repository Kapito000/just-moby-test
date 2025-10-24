using Features.DragAndDropSystems;
using UnityEngine;
using Zenject;

namespace Features.Towers
{
	public sealed class TowerPlacer : MonoBehaviour, ITowerPlace
	{
		[Inject] ITower _tower;

		public void Place(ItemPlaceData placeData)
		{
			_tower.Place(placeData);
		}
	}
}