using Features.Items;
using UnityEngine;

namespace Features.Towers
{
	public interface ITower
	{
		void AddNext(ItemPlaceData placeData);
		void AddFirst(ItemPlaceData placeData);
		bool IsTowerEmpty();
	}
}