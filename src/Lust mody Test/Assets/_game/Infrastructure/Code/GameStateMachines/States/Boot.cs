using System.Linq;
using Features.Input;
using Features.Items;
using Features.SaveLoads;
using Infrastructure.LifeCycleStateMachines;
using Zenject;

namespace Infrastructure.GameStateMachines.States
{
	public sealed class Boot : IState, IPayloadEnterState<BootPayload>
	{
		[Inject] ISaveLoadService _saveLoadService;
		[Inject] IGameStateMachine _gameStateMachine;
		[Inject] IBaseInputMapInit _baseInputMap;
		[Inject] IItemsDataCollectionProvider _itemsDataCollectionProvider;

		public void Enter(BootPayload payload)
		{
			_baseInputMap.Init();
			CubesListViewUpdate(payload);

			foreach (var bootEnter in payload.BootEnters)
				bootEnter.Execute();
			
			_saveLoadService.Load();

			_gameStateMachine.Enter<GameLoop>();
		}

		void CubesListViewUpdate(BootPayload payload)
		{
			var configs = _itemsDataCollectionProvider.Configs().ToArray();
			payload.MainMediator.CubesListViewUpdate(configs);
		}
	}
}