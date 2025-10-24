using Features.DragAndDropSystems;
using UnityEngine;
using Zenject;

namespace Features.Towers
{
	public sealed class TowerAreaPlacer : MonoBehaviour, IItemPlacer
	{
		[Inject] ITower _tower;

		IItemPlaceCondition[] _conditions;

		[Inject]
		void Construct(PlaceFirstCondition placeFirstCondition)
		{
			_conditions = new IItemPlaceCondition[]
			{
				placeFirstCondition,
			};
		}

		public void Place(ItemPlaceData placeData)
		{
			foreach (var condition in _conditions)
				if (condition.CanPlace(placeData) == false)
					return;

			_tower.AddFirst(placeData);
		}
	}
}