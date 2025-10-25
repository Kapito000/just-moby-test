using System;
using UniRx;

namespace Features.DeletingItems
{
	public interface IDestroyItemViewSystem
	{
		IObservable<Unit> ItemDestroyed { get; }
	}
}