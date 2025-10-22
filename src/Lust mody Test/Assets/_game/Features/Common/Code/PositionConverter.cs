using Extensions;
using UnityEngine;
using Zenject;

namespace Features.Common
{
	public sealed class PositionConverter : IPositionConverter
	{
		[Inject] ISceneData _sceneData;

		Camera Camera => _sceneData.Camera;

		public Vector2 ScreenToWorldPoint(Vector2 screenPoint)
		{
			return Camera.ScreenToWorldPoint(screenPoint).AsVector2();
		}
	}
}