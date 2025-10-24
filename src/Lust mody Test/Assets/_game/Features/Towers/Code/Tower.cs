using System.Collections.Generic;
using Features.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Features.Towers
{
	public sealed class Tower : MonoBehaviour, ITower
	{
		[SerializeField] float _verticalOffset = .5f;
		[SerializeField] float _horizontalOffset = .5f;

		[ReadOnly]
		[SerializeField] List<ItemPlacement> _placements;

		[Inject] IItemFactory _itemFactory;
		[Inject] IItemPlaceCondition[] _conditions;

		public void Place(ItemPlaceData placeData)
		{
			var (id, pos, size) = placeData;

			foreach (var condition in _conditions)
				if (condition.CanPlace(placeData) == false)
					return;

			if (IsTowerEmpty())
			{
				AddFirstItem(pos, id);
				return;
			}

			AddNextItem(pos, id);
		}

		void AddNextItem(Vector2 pos, string id)
		{
			var nextItemPosition = NextItemPosition();
			var newItem = CreateItem(pos, id);
			AddPlacement(nextItemPosition, newItem);
		}

		void AddFirstItem(Vector2 pos, string id)
		{
			var newItem = CreateItem(pos, id);
			AddPlacement(pos, newItem);
		}

		IItem CreateItem(Vector2 pos, string id)
		{
			var item = _itemFactory.Create(pos, id);
			var towerPlacer = item.gameObject.AddComponent<TowerPlacer>();
			return item;
		}

		void AddPlacement(Vector2 pos, IItem item)
		{
			var placement = new ItemPlacement()
			{
				Pos = pos,
				Item = item,
			};
			_placements.Add(placement);

			item.SetPosition(pos);
		}

		Vector2 NextItemPosition()
		{
			var placement = _placements[^1];
			var x = Random.Range(-_horizontalOffset, _horizontalOffset);
			var y = _verticalOffset;
			var pos = placement.Pos + new Vector2(x, y);
			return pos;
		}

		bool IsTowerEmpty() =>
			_placements.Count == 0;
	}
}