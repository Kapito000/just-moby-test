using System;
using Features.Cubes;
using UnityEngine;

namespace Features.Towers
{
	[Serializable]
	public struct CubePlacement
	{
		string _cubeDataId;
		Vector2 _pos;
		IGameCube _cube;
	}
}