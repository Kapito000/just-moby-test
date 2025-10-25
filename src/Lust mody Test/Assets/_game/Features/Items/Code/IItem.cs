using UnityEngine;

namespace Features.Items
{
	public interface IItem : IItemSize
	{
		string Id { get; }
		Sprite Skin { set; }
		GameObject GameObject { get; }

		void SetPosition(Vector2 pos);
	}
}