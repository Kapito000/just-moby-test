using Features.Input;
using Infrastructure.LifeCycleStateMachines;
using Zenject;

namespace Infrastructure.GameStateMachines.States
{
	public sealed class GameLoop : IState, IEnterState, IExitState
	{
		[Inject] IBaseInputMap _baseInputMap;
		[Inject] IMainInputService _inputService;

		public void Enter()
		{
			_inputService.Enable();
			_baseInputMap.Enable();
		}

		public void Exit()
		{
			_inputService.Disable();
			_baseInputMap.Disable();
		}
	}
}