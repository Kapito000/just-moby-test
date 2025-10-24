using System.Collections.Generic;
using Features.Items;
using UnityEngine;

namespace Features.Towers
{
	public interface ITower
	{
		void AddNext(ItemPlaceData placeData);
		void AddFirst(ItemPlaceData placeData);
		bool IsTowerEmpty();
		IReadOnlyList<ItemPlacement> Placements { get; }
	}
}