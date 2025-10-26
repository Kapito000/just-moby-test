using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Infrastructure;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Messages
{
	public sealed class MessageView : MonoBehaviour, IBootEnter
	{
		[SerializeField] TMP_Text _message;

		[Inject] IMessagesSystem _messagesSystem;

		Tween _animation;

		void IBootEnter.Execute()
		{
			const float duration = 2f;

			_messagesSystem.MessageCasted
				.Subscribe(text =>
				{
					_animation?.Kill();

					var color = _message.color;
					color.a = 1;
					_message.color = color;

					_message.text = text;

					_animation = _message.DOFade(0, duration);
				})
				.AddTo(this);
		}
	}
}