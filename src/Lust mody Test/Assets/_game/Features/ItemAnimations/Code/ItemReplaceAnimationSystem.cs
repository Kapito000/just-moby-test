using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Features.DeletingItems;
using Features.Towers;
using Infrastructure;
using UniRx;
using Zenject;

namespace Features.ItemAnimations
{
	public sealed class ItemReplaceAnimationSystem : IItemReplaceAnimationSystem,
		IBootEnter,
		IDisposable
	{
		[Inject] ITower _tower;
		[Inject] IDestroyItemViewSystem _destroyItemViewSystem;

		readonly CompositeDisposable _disposables = new();

		void IBootEnter.Execute()
		{
			_destroyItemViewSystem
				.ItemDestroyed
				.Subscribe(_ => OnItemDestroyed().Forget())
				.AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables?.Dispose();
		}

		async UniTaskVoid OnItemDestroyed()
		{
			const float duration = .2f;

			foreach (var placement in _tower.Placements)
			{
				var obj = placement.Item.GameObject;
				var pos = placement.Pos;
				await obj.transform
					.DOMove(pos, duration)
					.SetEase(Ease.Linear)
					.AsyncWaitForCompletion();
			}
		}
	}
}