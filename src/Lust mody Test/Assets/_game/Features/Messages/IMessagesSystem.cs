using System;
using Infrastructure;

namespace Features.Messages
{
	public interface IMessagesSystem : ISystem
	{
		IObservable<string> MessageCasted { get; }
	}
}