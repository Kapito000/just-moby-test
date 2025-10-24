using UnityEngine;

namespace Features.Items
{
	public interface IItemConfigDataProvider
	{
		string Id { get; }
		Sprite Sprite { get; }
	}
}