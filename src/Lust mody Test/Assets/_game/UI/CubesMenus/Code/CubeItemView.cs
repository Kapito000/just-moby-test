using Features.Cubes;
using UnityEngine;
using UnityEngine.UI;

namespace UI.CubesMenus
{
	public class CubeListItemView : MonoBehaviour
	{
		[SerializeField] Image _image;

		public void UpdateView(ICubeConfigDataProvider conf)
		{
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