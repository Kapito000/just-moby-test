using Constants;
using UnityEngine;
using Zenject;

namespace Features.SaveLoads
{
	[CreateAssetMenu(menuName = CreateAssetMenu.Installers + nameof(SaveServiceInstaller))]
	public sealed class SaveServiceInstaller : ScriptableObjectInstaller
	{
		public override void InstallBindings()
		{
			Container
				.BindInterfacesTo<SaveLoadService>()
				.AsSingle();
		}
	}
}