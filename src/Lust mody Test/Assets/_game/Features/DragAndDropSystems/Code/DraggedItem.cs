using Features.Items;
using UnityEngine;

namespace Features.DragAndDropSystems
{
	[RequireComponent(typeof(Item))]
	public sealed class DraggedItem : MonoBehaviour, IDraggedItem
	{
		[SerializeField] SpriteRenderer _skin;

		IItem _item;

		public IItemSize Size => _item;

		void Awake()
		{
			_item = GetComponent<Item>();
		}

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