using Infrastructure.GameStateMachines.States;
using UnityEngine;
using Zenject;
using Menu = Constants.CreateAssetMenu;

namespace Infrastructure.GameStateMachines
{
	[CreateAssetMenu(menuName = Menu.Installers + nameof(GameStatesInstaller))]
	public class GameStatesInstaller : ScriptableObjectInstaller
	{
		public override void InstallBindings()
		{
			BindStates();
			BindGameStateMachine();
		}

		void BindStates()
		{
			Container.BindInterfacesTo<Boot>().AsSingle();
			Container.BindInterfacesTo<GameLoop>().AsSingle();
			Container.BindInterfacesTo<Quit>().AsSingle();
		}

		void BindGameStateMachine()
		{
			Container
				.Bind<IGameStateMachine>()
				.To<GameStateMachine>()
				.FromNewComponentOnNewGameObject()
				.WithGameObjectName(nameof(GameStateMachine))
				.AsSingle();
		}
	}
}