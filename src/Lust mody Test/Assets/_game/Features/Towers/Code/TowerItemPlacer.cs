using Features.DragAndDropSystems;
using Features.DragAndDropSystems.ItemPlacers;
using UnityEngine;

namespace Features.Towers
{
	public sealed class TowerItemPlacer : MonoBehaviour, IItemPlacer
	{
		ITower _tower;

		public void Init(ITower tower)
		{
			_tower = tower;
		}

		public void Place(ItemPlaceData placeData)
		{
			_tower.AddNext(placeData);
		}
	}
}