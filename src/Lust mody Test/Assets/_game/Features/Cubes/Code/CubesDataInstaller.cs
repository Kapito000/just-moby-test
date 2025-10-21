using Features.Cubes.StaticData;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;
using Menu = Constants.CreateAssetMenu;

namespace Features.Cubes
{
	[CreateAssetMenu(menuName = Menu.Installers + nameof(CubesDataInstaller))]
	public class CubesDataInstaller : ScriptableObjectInstaller
	{
		[SerializeField] CubesConfigsCollection _cubesConfigsCollection;

		public override void InstallBindings()
		{
			BindCubesDataProvider();
			BindCubesConfigsCollection();
		}

		void BindCubesConfigsCollection()
		{
			Assert.IsNotNull(_cubesConfigsCollection);

			Container
				.Bind<CubesConfigsCollection>()
				.AsSingle()
				.WhenInjectedInto<ICubesDataProvider>();
		}

		void BindCubesDataProvider()
		{
			Container
				.BindInterfacesTo<CubesDataProvider>()
				.AsSingle();
		}
	}
}