using Features.Items;
using Features.Towers;
using UnityEngine;
using Zenject;

namespace Features.DragAndDropSystems
{
	public sealed class TowerPlacer : ITowerPlace
	{
		[Inject] ITower _tower;

		public void Place(Vector2 pos, IItem item)
		{
			_tower.Place(pos, item);
		}
	}
}