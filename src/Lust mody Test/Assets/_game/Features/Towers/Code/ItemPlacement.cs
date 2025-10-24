using System;
using Features.Items;
using UnityEngine;

namespace Features.Towers
{
	[Serializable]
	public struct ItemPlacement
	{
		public Vector2 Pos;
		public IItem Item;
	}
}