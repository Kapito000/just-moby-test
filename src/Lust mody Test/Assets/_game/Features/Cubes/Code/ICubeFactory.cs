using Infrastructure;
using UnityEngine;

namespace Features.Cubes
{
	public interface IGameCubeFactory : IFactory
	{
		GameCube Create();
		GameCube Create(Vector2 pos, string cubeId);
	}
}