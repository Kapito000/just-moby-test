using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Features.Messages
{
	public sealed class MessageSystemInstaller : MonoInstaller
	{
		[SerializeField] MessageView _view;

		public override void InstallBindings()
		{
			BindMessageView();

			Container
				.BindInterfacesTo<MessageProvider>()
				.AsSingle();
			Container
				.BindInterfacesTo<MessagesSystem>()
				.AsSingle();
		}

		void BindMessageView()
		{
			Assert.IsNotNull(_view);

			Container
				.BindInterfacesTo<MessageView>()
				.FromInstance(_view)
				.AsSingle();
		}
	}
}