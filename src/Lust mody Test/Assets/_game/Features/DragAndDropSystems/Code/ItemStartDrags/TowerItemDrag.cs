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

		Collider2D[] _resultCache = new Collider2D[8];

		readonly Subject<ITowerItem> _dragItemSubject = new();

		public IObservable<ITowerItem> DragItemStart => _dragItemSubject;

		Camera Camera => _sceneData.Camera;

		public void TryDrag(Vector2 screenPos)
		{
			var point = Camera.ScreenToWorldPoint(screenPos);
			var hitCount = Physics2D.OverlapPointNonAlloc(point, _resultCache);

			if (hitCount == 0)
				return;

			for (int i = 0; i < hitCount; i++)
			{
				var collider = _resultCache[i];

				if (collider.gameObject.TryGetComponent<ITowerItem>(out var item))
					_dragItemSubject.OnNext(item);
			}
		}

		public void Dispose()
		{
			_dragItemSubject.OnCompleted();
			_dragItemSubject?.Dispose();
		}
	}
}