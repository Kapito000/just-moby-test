using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Features.Common
{
	public sealed class CommonSceneInstaller : MonoInstaller
	{
		[SerializeField] Camera _camera;
		[SerializeField] EventSystem _eventSystem;
		[SerializeField] GraphicRaycaster _graphicRaycaster;

		public override void InstallBindings()
		{
			BindCommonSceneData();
		}

		void BindCommonSceneData()
		{
			Assert.IsNotNull(_camera);
			Assert.IsNotNull(_eventSystem);
			Assert.IsNotNull(_graphicRaycaster);

			Container
				.Bind<ISceneData>()
				.FromMethod(CreateCommonSceneData)
				.AsSingle()
				;
		}

		ISceneData CreateCommonSceneData(InjectContext arg)
		{
			var obj = new GameObject(nameof(SceneData));
			var commonSceneData = obj.AddComponent<SceneData>();

			commonSceneData.Camera = _camera;
			commonSceneData.EventSystem = _eventSystem;
			commonSceneData.GraphicRaycaster = _graphicRaycaster;

			return commonSceneData;
		}
	}
}