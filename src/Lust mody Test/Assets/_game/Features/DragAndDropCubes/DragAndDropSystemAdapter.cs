using Features.Input;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.DragAndDropServices
{
	public sealed class DragAndDropSystemAdapter : MonoBehaviour
	{
		[Inject] IBaseInputMap _baseInputMap;
		[Inject] IDragAndDropSystem _dragAndDropSystem;

		void Start()
		{
			_baseInputMap.StartDrag
				.Subscribe(_dragAndDropSystem.TryStartDrag)
				.AddTo(this);

			_baseInputMap.Drop
				.Subscribe(_dragAndDropSystem.TryDrop)
				.AddTo(this);

			Observable.EveryUpdate()
				.Subscribe(_ =>
					_dragAndDropSystem.PointerPosition = _baseInputMap.PointerPos)
				.AddTo(this);
		}
	}
}