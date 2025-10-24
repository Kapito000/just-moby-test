using System;
using System.Collections.Generic;
using Features.Common;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Features.DragAndDropSystems
{
	public sealed class NewItemDrag : IDisposable, INewItemDrag
	{
		[Inject] ISceneData _sceneData;
		
		readonly Subject<INewItem> _dragItemSubject = new();
		readonly List<RaycastResult> _raycastBuffer = new(16);

		public IObservable<INewItem> DragItemStart => _dragItemSubject;
		
		EventSystem EventSystem => _sceneData.EventSystem;
		GraphicRaycaster GraphicRaycaster => _sceneData.GraphicRaycaster;

		public void TryDrag(Vector2 screenPos)
		{
			PointerEventData pointerData = new PointerEventData(EventSystem)
			{
				position = screenPos,
			};

			_raycastBuffer.Clear();
			GraphicRaycaster.Raycast(pointerData, _raycastBuffer);

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