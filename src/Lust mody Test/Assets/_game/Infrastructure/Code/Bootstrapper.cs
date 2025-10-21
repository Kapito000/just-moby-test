using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States;
using Zenject;

namespace Infrastructure
{
	public class BootstrapInstaller : MonoInstaller, IInitializable
	{
		[Inject] IGameStateMachine _gameStateMachine;

		public void Initialize()
		{
			_gameStateMachine.Enter<Boot>();
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
	}
}