using UnityEngine;

namespace Features.Items
{
	public interface IItem : IItemSize
	{
		string Id { get; }
		Sprite Skin { set; }
		
		void SetPosition(Vector2 pos);
	}
}