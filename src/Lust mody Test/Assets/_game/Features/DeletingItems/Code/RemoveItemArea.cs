using Features.Items;
using Features.Towers;
using UnityEngine;
using Zenject;

namespace Features.DeletingItems
{
	public sealed class RemoveItemArea : MonoBehaviour, IRemoveItemArea
	{
		[Inject] ITower _tower;

		public void Remove(IItem item)
		{
			_tower.RemoveItem(item);
		}
	}
}