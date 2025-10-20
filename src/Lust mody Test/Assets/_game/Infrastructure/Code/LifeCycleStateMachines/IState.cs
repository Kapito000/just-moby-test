namespace Infrastructure.LifeCycleStateMachines
{
	public interface IState
	{ }

	public interface IEnterState
	{
		void Enter();
	}

	public interface IExitState
	{
		void Exit();
	}
	
	public interface IPayloadEnterState<in TPayload>
	{
		void Enter(TPayload payload);
	} 
}