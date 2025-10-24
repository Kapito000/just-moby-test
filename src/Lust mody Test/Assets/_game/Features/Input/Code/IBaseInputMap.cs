using System;
using UnityEngine;

namespace Features.Input
{
	public interface IBaseInputMap : IInputService
	{
		Vector2 PointerPos { get; }
		IObservable<Vector2> Drop { get; }
		IObservable<Vector2> StartDrag { get; }
		IObservable<Vector2> PointerPosChanged { get; }
	}

	public interface IBaseInputMapInit
	{
		void Init();
	}
}