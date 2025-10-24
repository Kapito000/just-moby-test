using Infrastructure;
using UnityEngine;

namespace Features.Items
{
	public interface IItemFactory : IFactory
	{
		IItem Create(Vector2 pos, string cubeId);
	}
}