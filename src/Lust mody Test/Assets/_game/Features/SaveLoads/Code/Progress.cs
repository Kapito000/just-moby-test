using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.SaveLoads
{
	[Serializable]
	public sealed class Progress
	{
		public List<TowerPlacement> TowerPlacements = new();
	}

	[Serializable]
	public struct TowerPlacement
	{
		public string Id;
		public Vector2 Pos;
	}
}