using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States;
using UI;
using Zenject;

namespace Infrastructure
{
	public class BootstrapInstaller : MonoInstaller
	{
		[Inject] IBootEnter[] _bootEnters;
		[Inject] IMainMediator _mainMediator;
		[Inject] IGameStateMachine _gameStateMachine;

		public override void Start()
		{
			EnterToBootGameState();
		}

		void EnterToBootGameState()
		{
			var bootPayload = new BootPayload()
			{
				MainMediator = _mainMediator,
				BootEnters = _bootEnters,
			};

			_gameStateMachine.Enter<Boot, BootPayload>(bootPayload);
		}
	}
}