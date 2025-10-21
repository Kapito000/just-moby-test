using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Features.Input
{
	public sealed class BaseInputMap : IBaseInputMap,
		IDisposable,
		IBaseInputMapInit
	{
		CompositeDisposable _disposables = new();

		[Inject] InputActions _inputActions;

		InputActions.BaseActions Map => _inputActions.Base;

		readonly Subject<Vector2> _drop = new();
		readonly Subject<Vector2> _startDrag = new();

		public Vector2 PointerPos { get; private set; }
		public IObservable<Vector2> Drop => _drop;
		public IObservable<Vector2> StartDrag => _startDrag;

		public void Init()
		{
			GetItemDownSubscription();
			GetItemUpSubscription();
			PointerPosSubscription();
		}

		public void Enable()
		{
			Map.Enable();
		}

		public void Disable()
		{
			Map.Disable();
		}

		public void Dispose()
		{
			_drop.OnCompleted();
			_startDrag.OnCompleted();

			_disposables?.Dispose();
		}

		void PointerPosSubscription()
		{
			Observable
				.FromEvent<InputAction.CallbackContext>(
					h => Map.Pointerpos.performed += h,
					h => Map.Pointerpos.performed -= h)
				.Subscribe(context => PointerPos = context.ReadValue<Vector2>())
				.AddTo(_disposables);
		}

		void GetItemUpSubscription()
		{
			Observable
				.FromEvent<InputAction.CallbackContext>(
					h => Map.Getitem.canceled += h,
					h => Map.Getitem.canceled -= h)
				.Subscribe(_ => _drop.OnNext(PointerPos))
				.AddTo(_disposables);
		}

		void GetItemDownSubscription()
		{
			Observable
				.FromEvent<InputAction.CallbackContext>(
					h => Map.Getitem.started += h,
					h => Map.Getitem.started -= h)
				.Subscribe(_ => _startDrag.OnNext(PointerPos))
				.AddTo(_disposables);
		}
	}
}