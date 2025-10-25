using System.Collections.Generic;
using Features.DragAndDropSystems.ItemStartDrags;
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

		public IReadOnlyList<ItemPlacement> Placements => _placements;

		IItemPlaceCondition[] _nextItemConditions;

		[Inject]
		void Construct(CameraViewCondition cameraViewCondition)
		{
			_nextItemConditions = new IItemPlaceCondition[]
			{
				cameraViewCondition,
			};
		}

		public void AddFirst(ItemPlaceData placeData)
		{
			var (id, pos, size) = placeData;
			var newItem = CreateItem(pos, id);
			AddPlacement(pos, newItem);
		}

		public void AddNext(ItemPlaceData placeData)
		{
			var id = placeData.Id;
			var nextPos = NextItemPosition();
			var newItem = CreateItem(nextPos, id);

			if (CanAddNextItem(nextPos, id, newItem) == false)
			{
				Destroy(newItem.gameObject);
				return;
			}

			AddPlacement(nextPos, newItem);
		}

		public bool IsTowerEmpty() =>
			_placements.Count == 0;

		bool CanAddNextItem(Vector2 pos, string id, Item newItem)
		{
			var placeData = new ItemPlaceData()
			{
				Id = id,
				Pos = pos,
				Size = newItem,
			};
			foreach (var condition in _nextItemConditions)
			{
				if (condition.CanPlace(placeData) == false)
					return false;
			}

			return true;
		}

		Item CreateItem(Vector2 pos, string id)
		{
			var item = _itemFactory.Create(pos, id);

			var obj = item.gameObject;

			var placer = obj.AddComponent<TowerItemPlacer>();
			placer.Init(this);

			var towerItem = obj.AddComponent<TowerItem>();
			towerItem.Id = id;

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
	}
}