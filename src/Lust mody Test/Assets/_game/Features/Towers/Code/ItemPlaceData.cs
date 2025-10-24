using Features.Items;
using UnityEngine;

namespace Features.Towers
{
	public struct ItemPlaceData
	{
		public string Id;
		public Vector2 Pos;
		public IItemSize Size;

		public void Deconstruct(out string id, out Vector2 pos, out IItemSize size)
		{
			id = Id;
			pos = Pos;
			size = Size;
		}
	}
}