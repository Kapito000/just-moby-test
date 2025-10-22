using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Features.Common
{
	public sealed class SceneData : MonoBehaviour, ISceneData
	{
		[field: SerializeField] public Camera Camera { get; set; }
		[field: SerializeField] public EventSystem EventSystem { get; set; }
		[field: SerializeField] public GraphicRaycaster GraphicRaycaster { get; set; }
	}
}