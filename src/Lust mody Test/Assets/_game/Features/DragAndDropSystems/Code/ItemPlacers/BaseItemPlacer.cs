using System;
using System.Collections.Generic;
using Features.Common;
using Features.Items;
using Features.Towers;
using UnityEngine;
using Zenject;

namespace Features.DragAndDropSystems.ItemPlacers
{
	public class BaseItemPlacer
	{
		[Inject] ISceneData _sceneData;

		Collider2D[] _resultCache = new Collider2D[8];
		IEnumerable<IItemPlaceCondition> _conditions;

		Camera Camera => _sceneData.Camera;

		public void AddConditions(IEnumerable<IItemPlaceCondition> conditions)
		{
			_conditions = conditions ?? Array.Empty<IItemPlaceCondition>();
		}

		public void Place(Vector2 screenPos, string id, IItemSize size)
		{
			var pos = Camera.ScreenToWorldPoint(screenPos);
			if (false == CanPlace<IItemPlacer>(pos, out var placer))
				return;

			var placeData = new ItemPlaceData()
			{
				Id = id,
				Pos = pos,
				Size = size,
			};

			foreach (var condition in _conditions)
				if (condition.CanPlace(placeData) == false)
					return;

			placer.Place(placeData);
		}

		bool CanPlace<T>(Vector2 point, out T place) where T : IItemPlacer
		{
			var hitCount = Physics2D.OverlapPointNonAlloc(point, _resultCache);

			if (hitCount == 0)
			{
				place = default;
				return false;
			}

			for (int i = 0; i < hitCount; i++)
			{
				var collider = _resultCache[i];
				if (collider.TryGetComponent<T>(out place))
				{
					return true;
				}
			}

			place = default;
			return false;
		}
	}
}