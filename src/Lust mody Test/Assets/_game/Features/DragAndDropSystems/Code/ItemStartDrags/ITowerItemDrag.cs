using System;
using UnityEngine;

namespace Features.DragAndDropSystems.ItemStartDrags
{
	public interface ITowerItemDrag
	{
		IObservable<ITowerItem> DragItemStart { get; }
		void TryDrag(Vector2 screenPos);
	}
}