using Zenject;

namespace Features.Input
{
	public sealed class InputService : IMainInputService
	{
		[Inject] InputActions _inputActions;

		public void Enable()
		{
			_inputActions.Enable();
		}

		public void Disable()
		{
			_inputActions.Disable();
		}
	}
}