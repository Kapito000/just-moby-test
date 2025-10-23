using UnityEngine;

namespace Features.Common
{
	public sealed class PhysicsRayCaster
	{
		Camera _camera;

		public PhysicsRayCaster()
		{ }

		public PhysicsRayCaster(Camera camera)
		{
			Build(camera);
		}

		public PhysicsRayCaster Build(Camera camera)
		{
			_camera = camera;
			return this;
		}

		public T CastRay<T>(Vector2 screenPos)
		{
			var origin = _camera.ScreenToWorldPoint(screenPos);

			var hit = Physics2D.Raycast(origin, Vector2.zero);
			if (hit.collider == null)
				return default;

			if (false == hit.collider.gameObject.TryGetComponent<T>(out var target))
				return default;

			return target;
		}
	}
}