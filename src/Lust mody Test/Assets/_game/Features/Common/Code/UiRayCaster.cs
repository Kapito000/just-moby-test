using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Features.Common
{
	public sealed class UiRayCaster
	{
		readonly List<RaycastResult> _raycastBuffer = new(16);

		EventSystem _eventSystem;
		GraphicRaycaster _graphicRaycaster;

		public UiRayCaster()
		{ }

		public UiRayCaster(
			EventSystem eventSystem,
			GraphicRaycaster graphicRaycaster)
		{
			Build(eventSystem, graphicRaycaster);
		}

		public UiRayCaster Build(
			EventSystem eventSystem,
			GraphicRaycaster graphicRaycaster)
		{
			_eventSystem = eventSystem;
			_graphicRaycaster = graphicRaycaster;
			return this;
		}

		public T CastRay<T>(Vector2 screenPos)
		{
			PointerEventData pointerData = new PointerEventData(_eventSystem)
			{
				position = screenPos,
			};

			_raycastBuffer.Clear();
			_graphicRaycaster.Raycast(pointerData, _raycastBuffer);

			for (int i = 0; i < _raycastBuffer.Count; i++)
			{
				var obj = _raycastBuffer[i].gameObject;

				if (obj.TryGetComponent<T>(out var target))
				{
					return target;
				}
			}

			return default;
		}
	}
}