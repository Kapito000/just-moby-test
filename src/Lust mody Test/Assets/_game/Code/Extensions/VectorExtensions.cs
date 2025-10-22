using UnityEngine;

namespace Extensions
{
	public static class VectorExtensions
	{
		public static Vector2 AsVector2(this Vector3 v) =>
			new(v.x, v.y);
	}
}