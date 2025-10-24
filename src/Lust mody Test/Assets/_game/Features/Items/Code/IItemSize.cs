using System.Collections.Generic;
using UnityEngine;

namespace Features.Items
{
	public interface IItemSize
	{
		IEnumerable<Vector2> SizePoints();
	}
}