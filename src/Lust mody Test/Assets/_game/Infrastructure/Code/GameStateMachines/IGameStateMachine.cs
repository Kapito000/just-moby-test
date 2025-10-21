using Infrastructure.LifeCycleStateMachines;

namespace Infrastructure.GameStateMachines
{
	public interface IGameStateMachine : IService
	{
		TState State<TState>() where TState : class, IState;
		void Enter<TState>() where TState : class, IState;

		public void Enter<TState, TPayload>(TPayload payload)
			where TState : class, IState, IPayloadEnterState<TPayload>;
	}
}