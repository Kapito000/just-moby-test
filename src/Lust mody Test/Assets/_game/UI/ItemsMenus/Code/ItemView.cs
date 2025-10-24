using Features.Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ItemsMenus
{
	public sealed class CubeListItemView : MonoBehaviour
	{
		[SerializeField] Image _image;

		public string CubeId { get; private set; }

		public void UpdateView(IItemConfigDataProvider conf)
		{
			CubeId = conf.Id;
			_image.sprite = conf.Sprite;
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