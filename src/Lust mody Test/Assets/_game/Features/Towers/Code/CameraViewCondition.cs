using Features.Common;
using UnityEngine;
using Zenject;

namespace Features.Towers
{
	public sealed class CameraViewCondition : IItemPlaceCondition
	{
		[Inject] ISceneData _sceneData;

		Camera Camera => _sceneData.Camera;

		public bool CanPlace(ItemPlaceData placeData)
		{
			var width = _sceneData.Camera.pixelWidth;
			var height = _sceneData.Camera.pixelHeight;

			foreach (var point in placeData.Size.SizePoints())
			{
				var screenPos = Camera.WorldToScreenPoint(point);
				if (screenPos.x < 0 || screenPos.y > height ||
				    screenPos.y < 0 || screenPos.x > width)
				{
					return false;
				}
			}

			return true;
		}
	}

	public sealed class PlaceFirstCondition : IItemPlaceCondition
	{
		[Inject] ITower _tower;

		public bool CanPlace(ItemPlaceData placeData) =>
			_tower.IsTowerEmpty();
	}
}