using Constants;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Features.ItemAnimations
{
	public sealed class AnimationInstaller : MonoInstaller
	{
		[SerializeField] Transform _removeArea;

		public override void InstallBindings()
		{
			BindItemDeletingSystem();
			BindRemoveItemAreaTransform();
			BindItemReplaceAnimationSystem();
		}

		void BindItemReplaceAnimationSystem()
		{
			Container
				.BindInterfacesTo<ItemReplaceAnimationSystem>()
				.AsSingle();
		}

		void BindRemoveItemAreaTransform()
		{
			Assert.IsNotNull(_removeArea);

			Container
				.Bind<Transform>()
				.WithId(DiKey.RemoveItemArea)
				.FromInstance(_removeArea);
		}

		void BindItemDeletingSystem()
		{
			Container
				.BindInterfacesTo<ItemDeletingAnimationSystem>()
				.AsSingle();
		}
	}
}