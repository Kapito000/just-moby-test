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

		Camera Camera => _sceneData.Camera;

		IEnumerable<IItemPlaceCondition> _conditions;

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

		bool CanPlace<T>(Vector2 origin, out T place) where T : IItemPlacer
		{
			var collider = Physics2D.OverlapPoint(origin);

			if (collider == null)
			{
				place = default;
				return false;
			}

			if (false == collider.TryGetComponent<T>(out place))
			{
				place = default;
				return false;
			}

			return true;
		}
	}
}