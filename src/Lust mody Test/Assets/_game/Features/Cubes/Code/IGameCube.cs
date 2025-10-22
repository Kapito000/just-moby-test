using UnityEngine;

namespace Features.Cubes
{
	public interface IGameCube
	{
		string DataId { get; }
		void RefreshSkin(Sprite sprite);
		void SetPosition(Vector3 pointerPosition);
	}
}