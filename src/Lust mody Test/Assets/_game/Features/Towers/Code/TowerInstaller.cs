using Features.DragAndDropSystems;
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
				.BindInterfacesAndSelfTo<CameraViewCondition>()
				.AsCached();
			Container
				.BindInterfacesAndSelfTo<UiGraphicsCondition>()
				.AsCached();
			Container
				.BindInterfacesAndSelfTo<PlaceFirstCondition>()
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