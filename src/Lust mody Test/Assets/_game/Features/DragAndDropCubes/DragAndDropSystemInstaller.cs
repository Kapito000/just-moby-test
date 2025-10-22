using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Features.DragAndDropCubes
{
	public sealed class DragAndDropSystemInstaller : MonoInstaller
	{
		[SerializeField] DragAndDropSystem _dragAndDropSystem;

		public override void InstallBindings()
		{
			BindDragAndDropService();
		}

		void BindDragAndDropService()
		{
			Assert.IsNotNull(_dragAndDropSystem);

			Container
				.BindInterfacesTo<DragAndDropSystem>()
				.FromInstance(_dragAndDropSystem)
				.AsSingle();
		}
	}
}