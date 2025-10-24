using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Features.DragAndDropSystems
{
	public sealed class NewItemDrag : IDisposable, INewItemDrag
	{
		readonly List<RaycastResult> _raycastBuffer = new(16);

		[Inject] EventSystem _eventSystem;
		[Inject] GraphicRaycaster _graphicRaycaster;

		Subject<INewItem> _dragItemSubject = new();

		public IObservable<INewItem> DragItemStart => _dragItemSubject;

		public void TryDrag(Vector2 screenPos)
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

				if (obj.TryGetComponent<INewItem>(out var item))
				{
					_dragItemSubject.OnNext(item);
					return;
				}
			}
		}

		public void Dispose()
		{
			_dragItemSubject.OnCompleted();
			_dragItemSubject?.Dispose();
		}
	}
}