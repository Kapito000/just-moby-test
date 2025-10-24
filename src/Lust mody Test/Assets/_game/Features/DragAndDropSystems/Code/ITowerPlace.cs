using Features.Items;
using UnityEngine;

namespace Features.DragAndDropSystems
{
	public interface ITowerPlace : IItemPlace
	{
		void Place(Vector2 pos, IItem item);
	}
}