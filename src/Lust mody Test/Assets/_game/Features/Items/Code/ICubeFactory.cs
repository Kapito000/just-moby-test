using Infrastructure;
using UnityEngine;

namespace Features.Items
{
	public interface IItemFactory : IFactory
	{
		Item Create(Vector2 pos, string cubeId);
	}
}