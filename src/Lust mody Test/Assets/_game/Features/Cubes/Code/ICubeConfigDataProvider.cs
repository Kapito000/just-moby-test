using UnityEngine;

namespace Features.Cubes
{
	public interface ICubeConfigDataProvider
	{
		string Id { get; }
		Sprite Sprite { get; }
	}
}