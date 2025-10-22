using UnityEngine;

namespace Features.Cubes
{
	public class GameCube : MonoBehaviour, IGameCube
	{
		[SerializeField] SpriteRenderer _spriteRenderer;

		public string DataId { get; set; }

		public void RefreshSkin(Sprite sprite)
		{
			_spriteRenderer.sprite = sprite;
		}

		public void SetPosition(Vector3 pointerPosition)
		{
			transform.position = pointerPosition;
		}
	}
}