using System.Collections.Generic;
using UnityEngine;

namespace Features.Items
{
	public class Item : MonoBehaviour, IItem
	{
		[SerializeField] SpriteRenderer _skin;

		public string Id { get; set; }
		public Sprite Skin
		{
			set => _skin.sprite = value;
		}

		[SerializeField] Transform[] _sizePoints;

		public void SetPosition(Vector2 pos)
		{
			transform.position = pos;
		}

		public IEnumerable<Vector2> SizePoints()
		{
			foreach (var sizePoint in _sizePoints)
				yield return sizePoint.position;
		}
	}
}