using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States;
using UI;
using Zenject;

namespace Infrastructure
{
	public class BootstrapInstaller : MonoInstaller, IInitializable
	{
		[Inject] IMainMediator _mainMediator;
		[Inject] IGameStateMachine _gameStateMachine;

		public void Initialize()
		{
			EnterToBootGameState();
		}

		public override void InstallBindings()
		{
			BindInitializable();
		}

		void BindInitializable()
		{
			Container
				.BindInterfacesTo<BootstrapInstaller>()
				.FromInstance(this);
		}

		void EnterToBootGameState()
		{
			var bootPayload = new BootPayload()
			{
				MainMediator = _mainMediator,
			};
			_gameStateMachine.Enter<Boot, BootPayload>(bootPayload);
		}
	}
}