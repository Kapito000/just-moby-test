using System;
using Features.Common;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.DragAndDropSystems.ItemStartDrags
{
	public sealed class TowerItemDrag : ITowerItemDrag, IDisposable
	{
		[Inject] ISceneData _sceneData;

		readonly Subject<ITowerItem> _dragItemSubject = new();

		public IObservable<ITowerItem> DragItemStart => _dragItemSubject;

		Camera Camera => _sceneData.Camera;

		public void TryDrag(Vector2 screenPos)
		{
			var point = Camera.ScreenToWorldPoint(screenPos);
			var collider = Physics2D.OverlapPoint(point);

			if (collider.gameObject.TryGetComponent<ITowerItem>(out var item))
				_dragItemSubject.OnNext(item);
		}

		public void Dispose()
		{
			_dragItemSubject.OnCompleted();
			_dragItemSubject?.Dispose();
		}
	}
}