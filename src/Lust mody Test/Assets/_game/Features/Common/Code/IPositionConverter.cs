using UnityEngine;

namespace Features.Common
{
	public interface IPositionConverter
	{
		Vector2 ScreenToWorldPoint(Vector2 screenPoint);
	}
}