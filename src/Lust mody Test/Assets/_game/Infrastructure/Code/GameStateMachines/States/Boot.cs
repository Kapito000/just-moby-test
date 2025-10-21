using Features.Input;
using Infrastructure.LifeCycleStateMachines;
using Zenject;

namespace Infrastructure.GameStateMachines.States
{
	public sealed class Boot : IState, IEnterState
	{
		[Inject] IGameStateMachine _gameStateMachine;
		[Inject] IBaseInputMapInit _baseInputMap;

		public void Enter()
		{
			_baseInputMap.Init();
			_gameStateMachine.Enter<GameLoop>();
		}
	}
}