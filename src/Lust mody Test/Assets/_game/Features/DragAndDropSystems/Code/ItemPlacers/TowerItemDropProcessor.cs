using Features.Common;
using Features.DeletingItems;
using Features.Items;
using UnityEngine;
using Zenject;

namespace Features.DragAndDropSystems.ItemPlacers
{
	public sealed class TowerItemDropProcessor : ITowerItemDropProcessor
	{
		[Inject] ISceneData _sceneData;

		Camera Camera => _sceneData.Camera;

		public void Process(Vector2 screenPos, IItem item)
		{
			var pos = Camera.ScreenToWorldPoint(screenPos);
			
			if (CanPlace<IRemoveItemArea>(pos, out var removeArea))
			{
				removeArea.Remove(item);
				return;
			}
		}

		bool CanPlace<T>(Vector2 origin, out T place)
		{
			var collider = Physics2D.OverlapPoint(origin);

			if (collider == null)
			{
				place = default;
				return false;
			}

			if (false == collider.TryGetComponent<T>(out place))
			{
				place = default;
				return false;
			}

			return true;
		}
	}
}