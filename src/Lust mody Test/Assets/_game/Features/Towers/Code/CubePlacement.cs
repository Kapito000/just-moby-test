using System;
using Features.Cubes;
using UnityEngine;

namespace Features.Towers
{
	[Serializable]
	public struct CubePlacement
	{
		public Vector2 Pos;
		public GameCube Cube;
	}
}