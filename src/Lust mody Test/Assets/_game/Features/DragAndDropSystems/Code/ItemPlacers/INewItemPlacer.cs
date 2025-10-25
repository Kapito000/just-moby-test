using Features.Items;
using UnityEngine;

namespace Features.DragAndDropSystems.ItemPlacers
{
	public interface INewItemPlacer
	{
		void Place(Vector2 screenPos, string id, IItemSize size);
	}
}