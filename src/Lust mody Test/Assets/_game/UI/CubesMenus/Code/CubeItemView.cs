using Features.Cubes;
using Features.DragAndDropServices;
using UnityEngine;
using UnityEngine.UI;

namespace UI.CubesMenus
{
	public sealed class CubeListItemView : MonoBehaviour, IDragTarget
	{
		[SerializeField] Image _image;

		public string CubeId { get; private set; }

		public void UpdateView(ICubeConfigDataProvider conf)
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