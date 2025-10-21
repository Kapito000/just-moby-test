using UnityEngine;
using Zenject;
using Menu = Constants.CreateAssetMenu;

namespace Features.Input
{
	[CreateAssetMenu(menuName = Menu.Installers + nameof(InputServiceInstaller))]
	public sealed class InputServiceInstaller : ScriptableObjectInstaller
	{
		public override void InstallBindings()
		{
			BindInputMap();
			BindInputActions();
			BindMainInputService();
		}

		void BindMainInputService()
		{
			Container
				.BindInterfacesTo<InputService>()
				.AsSingle();
		}

		void BindInputMap()
		{
			Container
				.BindInterfacesTo<BaseInputMap>()
				.AsSingle();
		}

		void BindInputActions()
		{
			Container
				.Bind<InputActions>()
				.AsSingle()
				.WhenInjectedInto<IInputService>();
		}
	}
}