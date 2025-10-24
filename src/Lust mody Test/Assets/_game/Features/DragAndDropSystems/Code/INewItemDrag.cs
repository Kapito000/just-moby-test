using System;
using UnityEngine;

namespace Features.DragAndDropSystems
{
	public interface INewItemDrag
	{
		IObservable<INewItem> DragItemStart { get; }
		void TryDrag(Vector2 screenPos);
	}
}