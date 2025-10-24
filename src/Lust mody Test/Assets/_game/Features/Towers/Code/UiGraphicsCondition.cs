using System.Collections.Generic;
using Features.Common;
using Features.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Features.Towers
{
	public sealed class UiGraphicsCondition : IItemPlaceCondition
	{
		[Inject] ISceneData _sceneData;

		List<RaycastResult> _rayCastResults = new(8);

		Camera Camera => _sceneData.Camera;
		EventSystem EventSystem => _sceneData.EventSystem;
		GraphicRaycaster Raycaster => _sceneData.GraphicRaycaster;

		public bool CanPlace(ItemPlaceData placeData)
		{
			foreach (var point in placeData.Size.SizePoints())
			{
				var screenPos = Camera.WorldToScreenPoint(point);

				PointerEventData eventData = new PointerEventData(EventSystem)
					{ position = screenPos };
				_rayCastResults.Clear();

				Raycaster.Raycast(eventData, _rayCastResults);

				foreach (var result in _rayCastResults)
					if (result.gameObject != null)
						return false;
			}

			return true;
		}
	}
}