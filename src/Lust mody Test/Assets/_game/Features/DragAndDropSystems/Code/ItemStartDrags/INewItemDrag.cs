using System;
using UnityEngine;

namespace Features.DragAndDropSystems.ItemStartDrags
{
	public interface INewItemDrag
	{
		IObservable<INewItem> DragItemStart { get; }
		void TryDrag(Vector2 screenPos);
	}
}