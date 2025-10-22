using Infrastructure;
using UnityEngine;

namespace Features.Cubes
{
	public interface IGameCubeFactory : IFactory
	{
		IGameCube Create(Vector2 pos, string cubeId);
	}
}