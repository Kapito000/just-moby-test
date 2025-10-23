using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Features.Cubes
{
	public class GameCube : MonoBehaviour, IGameCube
	{
		[SerializeField] SpriteRenderer _spriteRenderer;
		[SerializeField] Transform[] _sizePoints;

		public string DataId { get; set; }

		void Awake()
		{
			Assert.IsTrue(_sizePoints.Length > 0);
		}

		public void RefreshSkin(Sprite sprite)
		{
			_spriteRenderer.sprite = sprite;
		}

		public void SetPosition(Vector3 pointerPosition)
		{
			transform.position = pointerPosition;
		}

		public IEnumerable<Vector2> SizePoints()
		{
			foreach (var sizePoint in _sizePoints)
				yield return sizePoint.position;
		}

		public void Destroy()
		{
			Destroy(gameObject);
		}
	}
}