using UnityEngine;

namespace Features.DragAndDropSystems.ItemHolders
{
	public sealed class ItemHolder : IItemHolder
	{
		bool _isHold;

		public bool IsHold => _isHold;

		public void Hold(bool value)
		{
			_isHold = value;
		}

		public void Accept(IItemHolderVisitor visitor)
		{
			Debug.LogError($"Has no a realization for \"{nameof(ItemHolder)}\".");
		}
	}
}