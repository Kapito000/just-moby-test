using UnityEngine;

namespace Features.Common
{
	public sealed class PhysicsRayCaster
	{
		public static T CastRay<T>(Vector2 origin)
		{
			var hit = Physics2D.Raycast(origin, Vector2.zero);
			if (hit.collider == null)
				return default;

			if (false == hit.collider.gameObject.TryGetComponent<T>(out var target))
				return default;

			return target;
		}
	}
}