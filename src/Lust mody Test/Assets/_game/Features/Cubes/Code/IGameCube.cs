using System.Collections.Generic;
using UnityEngine;

namespace Features.Cubes
{
	public interface IGameCube
	{
		string DataId { get; set; }
		Vector2 Position { get; set; }
		void RefreshSkin(Sprite sprite);
		IEnumerable<Vector2> SizePoints();
		void Destroy();
		void Enable(bool enable);
	}
}