using UnityEngine;

namespace Features.Common
{
	public sealed class PhysicsRayCaster
	{
		public static T CastRay<T>(Vector2 origin)
		{
			var collider = Physics2D.OverlapPoint(origin);
			if (collider == null)
				return default;

			if (false == collider.gameObject.TryGetComponent<T>(out var target))
				return default;

			return target;
		}
	}
}