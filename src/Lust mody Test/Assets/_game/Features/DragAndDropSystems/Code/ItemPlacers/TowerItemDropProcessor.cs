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

		Collider2D[] _resultCache = new Collider2D[8];

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

		bool CanPlace<T>(Vector2 point, out T place)
		{
			var hitCount = Physics2D.OverlapPointNonAlloc(point, _resultCache);

			if (hitCount == 0)
			{
				place = default;
				return false;
			}

			for (int i = 0; i < hitCount; i++)
			{
				var collider = _resultCache[i];
				if (collider.TryGetComponent<T>(out place))
				{
					return true;
				}
			}

			place = default;
			return true;
		}
	}
}