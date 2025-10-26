using System.Collections.Generic;

namespace Features.Messages
{
	public sealed class MessageProvider : IMessagesProvider
	{
		Dictionary<string, string> _messages = new()
		{
			{ MessageKey.CreateNewItem, "You have put a new cube to the filed." },
			{ MessageKey.RemoveItem, "You have removed a cube." },
		};

		public string Message(string key)
		{
			return _messages[key];
		}
	}
}