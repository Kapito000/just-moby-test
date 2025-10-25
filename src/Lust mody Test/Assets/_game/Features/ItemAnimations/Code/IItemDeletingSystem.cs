using System;
using Features.Items;
using Infrastructure;

namespace Features.ItemAnimations
{
	public interface IItemDeletingAnimationSystem : ISystem
	{
		IObservable<IItem> AnimationCompleted { get; }
	}
}