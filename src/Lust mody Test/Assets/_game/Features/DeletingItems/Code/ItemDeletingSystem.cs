using System;
using Constants;
using DG.Tweening;
using Features.Items;
using Features.Towers;
using Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Features.DeletingItems
{
	public sealed class ItemDeletingSystem : IItemDeletingSystem, IBootEnter, IDisposable
	{
		[Inject] ITower _tower;
		[Inject(Id = DiKey.RemoveItemArea)]
		Transform _removeArea;

		CompositeDisposable _disposables = new();

		public void Execute()
		{
			_tower.RemovedItem
				.Subscribe(OnTowerItemRemoved)
				.AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Dispose();
		}

		void OnTowerItemRemoved(IItem item)
		{
			const float duration = 1f;

			var obj = item.GameObject;

			DOTween.Sequence()
				.Append(obj.transform.DOMove(_removeArea.position, duration))
				.Append(obj.transform.DOScale(Vector3.zero, duration))
				.OnComplete(() => Object.Destroy(obj))
				;
		}
	}
}