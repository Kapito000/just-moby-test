using Features.Items;
using UnityEngine;

namespace Features.DragAndDropSystems.ItemStartDrags
{
	public sealed class TowerItem : MonoBehaviour, ITowerItem
	{
		public string Id { get; set; }
		public IItem Item { get; set; }
	}
}