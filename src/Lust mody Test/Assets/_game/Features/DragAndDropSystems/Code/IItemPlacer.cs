using Features.Items;
using UnityEngine;

namespace Features.DragAndDropSystems
{
	public interface INewItemPlacer
	{
		void Place(Vector2 screenPos, string id, IItemSize size);
	}
}