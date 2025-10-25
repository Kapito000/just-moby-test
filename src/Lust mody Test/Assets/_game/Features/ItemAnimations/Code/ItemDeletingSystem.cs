using System;
using Constants;
using DG.Tweening;
using Features.Items;
using Features.Towers;
using Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.ItemAnimations
{
	public sealed class ItemDeletingAnimationSystem : IItemDeletingAnimationSystem, IBootEnter, IDisposable
	{
		[Inject] ITower _tower;
		[Inject(Id = DiKey.RemoveItemArea)]
		Transform _removeArea;

		CompositeDisposable _disposables = new();

		Subject<IItem> _animationCompleted = new();

		public IObservable<IItem> AnimationCompleted => _animationCompleted;

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
			const float duration = .2f;

			var obj = item.GameObject;

			DOTween.Sequence()
				.Join(obj.transform.DOMove(_removeArea.position, duration).SetEase(Ease.Linear))
				.Join(obj.transform.DOScale(Vector3.zero, duration).SetEase(Ease.Linear))
				.OnComplete(() => _animationCompleted.OnNext(item))
				;
		}
	}
}