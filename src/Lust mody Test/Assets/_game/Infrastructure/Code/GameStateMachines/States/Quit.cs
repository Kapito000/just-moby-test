using Features.SaveLoads;
using Infrastructure.LifeCycleStateMachines;
using Zenject;

namespace Infrastructure.GameStateMachines.States
{
	public sealed class Quit : IState, IEnterState
	{
		[Inject] ISaveLoadService _saveLoadService;

		public void Enter()
		{
			_saveLoadService.Save();
		}
	}
}