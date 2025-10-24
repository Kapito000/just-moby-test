using Features.Items;
using UnityEngine;

namespace Features.Towers
{
	public interface ITower
	{
		void Place(ItemPlaceData placeData);
	}
}