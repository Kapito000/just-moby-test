using UnityEngine;

namespace Features.Common
{
	public sealed class PhysicsRayCaster
	{
		public T CastRay<T>(Camera camera, Vector2 screenPos)
		{
			Ray ray = camera.ScreenPointToRay(screenPos);

			if (false == Physics.Raycast(ray, out var hit))
				return default;

			if (false == hit.collider.gameObject.TryGetComponent<T>(out var target))
				return default;

			return target;
		}
	}
}