using System;
using System.Collections.Generic;
using System.Linq;
using Features.DragAndDropSystems.ItemStartDrags;
using Features.Items;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Features.Towers
{
	public sealed class Tower : MonoBehaviour, ITower, IDisposable
	{
		[SerializeField] float _verticalOffset = .5f;
		[SerializeField] float _horizontalOffset = .5f;

		[ReadOnly]
		[SerializeField] List<ItemPlacement> _placements;

		[Inject] IItemFactory _itemFactory;

		Subject<IItem> _removedItem = new();

		public IObservable<IItem> RemovedItem => _removedItem;
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

		public void JustAddNext(ItemPlaceData placeData)
		{
			var id = placeData.Id;
			var nextPos = placeData.Pos;
			var newItem = CreateItem(nextPos, id);
			
			AddPlacement(nextPos, newItem);
		}

		public void RemoveItem(IItem item)
		{
			var removesIndex = _placements.FindIndex(p => p.Item == item);
			if (removesIndex < 0)
				return;

			var positions = _placements
				.Select(p => p.Pos)
				.ToArray();

			for (int i = removesIndex + 1; i < _placements.Count; i++)
			{
				var placement = _placements[i];
				_placements[i] = new ItemPlacement()
				{
					Pos = positions[i - 1],
					Item = _placements[i].Item,
				};
			}

			_placements.RemoveAt(removesIndex);
			_removedItem.OnNext(item);
		}

		public bool IsTowerEmpty() =>
			_placements.Count == 0;

		public void Dispose()
		{
			_removedItem.OnCompleted();
			_removedItem?.Dispose();
		}

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
			towerItem.Item = item;

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