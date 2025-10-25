using System;
using Features.ItemAnimations;
using Infrastructure;
using UniRx;
using Zenject;

namespace Features.DeletingItems
{
	public class DestroyItemViewSystem : IDestroyItemViewSystem, IBootEnter, IDisposable
	{
		[Inject] IItemDeletingAnimationSystem _itemDeletingAnimationAnimationSystem;

		CompositeDisposable _disposables = new();
		Subject<Unit> _itemDestroyed = new();

		public IObservable<Unit> ItemDestroyed => _itemDestroyed;

		void IBootEnter.Execute()
		{
			_itemDeletingAnimationAnimationSystem
				.AnimationCompleted
				.Subscribe(item =>
				{
					UnityEngine.Object.Destroy(item.GameObject);
					_itemDestroyed.OnNext(Unit.Default);
				})
				.AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Dispose();
		}
	}
}