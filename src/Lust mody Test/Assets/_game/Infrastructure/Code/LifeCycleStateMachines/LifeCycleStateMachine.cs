using Common;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Infrastructure.LifeCycleStateMachines
{
	public class LifeCycleStateMachine : MonoBehaviour
	{
#if UNITY_EDITOR
		[ReadOnly]
		[SerializeField] string _currentStateName;
#endif

		StateWrapper _currentStateWrapper;

		readonly TypeLocator<IState> _states = new();

		[Inject]
		void Construct(IState[] states)
		{
			_states.Add(states);
		}

		public void Enter<TState>() where TState : class, IState
		{
			_currentStateWrapper.Exit();
			ChangeState<TState>();
			_currentStateWrapper.Enter();

			EditChangeCurrentStateName();
		}

		public void Enter<TState, TPayload>(TPayload payload)
			where TState : class, IState, IPayloadEnterState<TPayload>
		{
			_currentStateWrapper.Exit();
			ChangeState<TState>();
			_currentStateWrapper.EnterPayload(payload);

			EditChangeCurrentStateName();
		}

		public TState State<TState>() where TState : class, IState =>
			_states.Get<TState>();

		void ChangeState<TState>() where TState : class, IState
		{
			var state = State<TState>();
			_currentStateWrapper.State = state;
			_currentStateWrapper.EnterState = state as IEnterState;
			_currentStateWrapper.ExitState = state as IExitState;
		}

		void EditChangeCurrentStateName()
		{
#if UNITY_EDITOR
			_currentStateName = _currentStateWrapper.State.GetType().Name;
#endif
		}

		struct StateWrapper
		{
			public IState State;
			public IExitState ExitState;
			public IEnterState EnterState;

			public void Exit()
			{
				ExitState?.Exit();
			}

			public void Enter()
			{
				EnterState?.Enter();
			}

			public void EnterPayload<TPayload>(TPayload payload)
			{
				var state = State as IPayloadEnterState<TPayload>;
				state.Enter(payload);
			}
		}
	}
}