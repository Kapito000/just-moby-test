using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Features.Cubes
{
	public class GameCube : MonoBehaviour
	{
		[SerializeField] SpriteRenderer _spriteRenderer;
		[SerializeField] Transform[] _sizePoints;

		public string DataId { get; set; }
		public Vector2 Position
		{
			get => transform.position;
			set => transform.position = value;
		}

		void Awake()
		{
			Assert.IsTrue(_sizePoints.Length > 0);
		}

		public void RefreshSkin(Sprite sprite)
		{
			_spriteRenderer.sprite = sprite;
		}

		public IEnumerable<Vector2> SizePoints()
		{
			foreach (var sizePoint in _sizePoints)
				yield return sizePoint.position;
		}

		public void Enable(bool enable)
		{
			gameObject.SetActive(enable);
		}

		public void Destroy()
		{
			Destroy(gameObject);
		}
	}
}