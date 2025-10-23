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