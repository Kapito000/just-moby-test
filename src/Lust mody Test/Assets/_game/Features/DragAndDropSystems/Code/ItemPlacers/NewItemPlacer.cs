using Features.Items;
using Features.Towers;
using UnityEngine;
using Zenject;

namespace Features.DragAndDropSystems.ItemPlacers
{
	public sealed class NewItemPlacer : INewItemPlacer
	{
		readonly BaseItemPlacer _baseItemPlacer;

		public NewItemPlacer(
			BaseItemPlacer baseItemPlacer,
			UiGraphicsCondition graphicsCondition,
			CameraViewCondition cameraViewCondition)
		{
			baseItemPlacer.AddConditions(new IItemPlaceCondition[]
			{
				graphicsCondition,
				cameraViewCondition,
			});
			
			_baseItemPlacer = baseItemPlacer;
		}

		public void Place(Vector2 screenPos, string id, IItemSize size)
		{
			_baseItemPlacer.Place(screenPos, id, size);
		}
	}
}