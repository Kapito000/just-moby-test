using Features.Items;
using UnityEngine;

namespace Features.DragAndDropSystems.ItemPlacers
{
	public interface ITowerItemDropProcessor
	{
		void Process(Vector2 screenPos, IItem item);
	}
}