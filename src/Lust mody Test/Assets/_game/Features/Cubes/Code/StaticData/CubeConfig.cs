using System.Diagnostics;
using UnityEngine;
using Menu = Constants.CreateAssetMenu;

namespace Features.Cubes.StaticData
{
	[CreateAssetMenu(menuName = Menu.StaticData + nameof(CubeConfig))]
	public sealed class CubeConfig : ScriptableObject
	{
		public string Id;
		public Sprite Sprite;

		[Conditional("UNITY_EDITOR")]
		[Sirenix.OdinInspector.Button]
		void GenerateId()
		{
			UnityEditor.Undo.RecordObject(this, $"{nameof(CubeConfig)} {nameof(Id)} has been changed.");
			Id = System.Guid.NewGuid().ToString();
			UnityEditor.EditorUtility.SetDirty(this);
		}
	}
}