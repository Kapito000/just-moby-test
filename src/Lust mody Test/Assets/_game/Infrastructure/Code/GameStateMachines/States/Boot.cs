using System.Linq;
using Features.Cubes;
using Features.Input;
using Infrastructure.LifeCycleStateMachines;
using Zenject;

namespace Infrastructure.GameStateMachines.States
{
	public sealed class Boot : IState, IPayloadEnterState<BootPayload>
	{
		[Inject] IGameStateMachine _gameStateMachine;
		[Inject] IBaseInputMapInit _baseInputMap;
		[Inject] ICubesDataCollectionProvider _cubesDataCollectionProvider;

		public void Enter(BootPayload payload)
		{
			_baseInputMap.Init();
			CubesListViewUpdate(payload);
			_gameStateMachine.Enter<GameLoop>();
		}

		void CubesListViewUpdate(BootPayload payload)
		{
			var configs = _cubesDataCollectionProvider.Configs().ToArray();
			payload.MainMediator.CubesListViewUpdate(configs);
		}
	}
}