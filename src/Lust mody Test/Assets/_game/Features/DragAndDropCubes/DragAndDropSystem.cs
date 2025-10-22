using Features.Common;
using Features.Cubes;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.DragAndDropCubes
{
	public sealed class DragAndDropSystem : MonoBehaviour, IDragAndDropSystem
	{
		[ReadOnly]
		[SerializeField] bool _isHold;

		[Inject] ISceneData _sceneData;
		[Inject] IGameCubeFactory _gameCubeFactory;
		[Inject] IPositionConverter _positionConverter;

		IGameCube _capturedCube;
		UiRayCaster _uiRayCaster;

		public Vector2 PointerPosition { get; set; }

		void Awake()
		{
			Observable.EveryUpdate()
				.Where(_ => _isHold)
				.Subscribe(_ => { MoveCube(); })
				.AddTo(this);

			_uiRayCaster = new UiRayCaster(
				_sceneData.EventSystem, _sceneData.GraphicRaycaster);
		}

		public void TryStartDrag(Vector2 screenPos)
		{
			var dragTarget = _uiRayCaster.CastRay<IDragTarget>(screenPos);
			if (dragTarget == null)
				return;

			var id = dragTarget.CubeId;
			var pos = _positionConverter.ScreenToWorldPoint(screenPos);
			_capturedCube = _gameCubeFactory.Create(pos, id);
			_isHold = true;
		}

		public void TryDrop(Vector2 screenPos)
		{ }

		void MoveCube()
		{
			var pos = _positionConverter.ScreenToWorldPoint(PointerPosition);
			_capturedCube.SetPosition(pos);
		}
	}
}