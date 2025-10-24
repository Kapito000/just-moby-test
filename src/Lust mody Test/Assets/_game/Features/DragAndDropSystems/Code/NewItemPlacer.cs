using System;
using System.Collections.Generic;
using Features.Common;
using Features.Items;
using Features.Towers;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Features.DragAndDropSystems
{
	public sealed class NewItemPlacer : INewItemPlacer
	{
		readonly List<RaycastResult> _raycastBuffer = new(16);

		[Inject] ISceneData _sceneData;

		Subject<IItemPlace> _placeItem = new();

		IObservable<IItemPlace> PlaceItem => _placeItem;

		public void Place(Vector2 screenPos, string id, IItemSize size)
		{
			var pos = _sceneData.Camera.ScreenToWorldPoint(screenPos);
			if (false == CanPlace<ITowerPlace>(pos, out var towerPlacer))
				return;

			var placeData = new ItemPlaceData()
			{
				Id = id,
				Pos = pos,
				Size = size,
			};
			towerPlacer.Place(placeData);
		}

		bool CanPlace<T>(Vector2 origin, out T place) where T : IItemPlace
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