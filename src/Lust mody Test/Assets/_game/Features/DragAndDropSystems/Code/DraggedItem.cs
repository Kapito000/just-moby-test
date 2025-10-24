using UnityEngine;

namespace Features.DragAndDropSystems
{
	public sealed class DraggedItem : MonoBehaviour, IDraggedItem
	{
		[SerializeField] SpriteRenderer _skin;

		public void Move(Vector2 pos)
		{
			transform.position = pos;
		}

		public void SetSkin(Sprite skin)
		{
			_skin.sprite = skin;
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}