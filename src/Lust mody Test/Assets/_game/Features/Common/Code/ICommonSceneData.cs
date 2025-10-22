using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Features.Common
{
	public interface ISceneData
	{
		Camera Camera { get; }
		EventSystem EventSystem { get; }
		GraphicRaycaster GraphicRaycaster { get; }
	}
}