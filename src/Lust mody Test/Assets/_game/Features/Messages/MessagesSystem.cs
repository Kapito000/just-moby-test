using System;
using Features.Towers;
using Infrastructure;
using UniRx;
using Zenject;

namespace Features.Messages
{
	public sealed class MessagesSystem : IMessagesSystem, IBootEnter, IDisposable
	{
		[Inject] ITower _tower;
		[Inject] IMessagesProvider _messagesProvider;

		CompositeDisposable _disposables = new();

		Subject<string> _messageCasted = new();
		public IObservable<string> MessageCasted => _messageCasted;

		void IBootEnter.Execute()
		{
			_tower.ItemPlaced
				.Do(_ => CastMessage(MessageKey.CreateNewItem))
				.Subscribe()
				.AddTo(_disposables);

			_tower.RemovedItem
				.Do(_ => CastMessage(MessageKey.RemoveItem))
				.Subscribe()
				.AddTo(_disposables);
		}

		void CastMessage(string key)
		{
			var msg = _messagesProvider.Message(key);
			_messageCasted.OnNext(msg);
		}

		public void Dispose()
		{
			_disposables?.Dispose();
		}
	}
}