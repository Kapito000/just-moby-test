using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Features.Towers
{
	public sealed class TowerInstaller : MonoInstaller
	{
		[SerializeField] Tower _tower;

		public override void InstallBindings()
		{
			BindTower();
			BindPlaceConditions();
		}

		void BindPlaceConditions()
		{
			Container
				.BindInterfacesTo<CameraViewCondition>()
				.AsCached();

			Container
				.BindInterfacesTo<UiGraphicsCondition>()
				.AsCached();
		}

		void BindTower()
		{
			Assert.IsNotNull(_tower);

			Container
				.BindInterfacesTo<Tower>()
				.FromInstance(_tower)
				.AsSingle();
		}
	}
}