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
			BindGameCubeFactory();
			BindCubesDataProvider();
			BindCubesConfigsCollection();
		}

		void BindGameCubeFactory()
		{
			Container
				.BindInterfacesTo<GameCubeFactory>()
				.AsSingle();
		}

		void BindCubesConfigsCollection()
		{
			Assert.IsNotNull(_cubesConfigsCollection);

			Container
				.Bind<CubesConfigsCollection>()
				.FromInstance(_cubesConfigsCollection)
				.AsSingle()
				.WhenInjectedInto<ICubesDataCollectionProvider>();
		}

		void BindCubesDataProvider()
		{
			Container
				.BindInterfacesTo<CubesDataCollectionProvider>()
				.AsSingle();
		}
	}
}