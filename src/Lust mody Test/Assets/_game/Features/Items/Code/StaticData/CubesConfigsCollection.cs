using UnityEngine;
using Menu = Constants.CreateAssetMenu;

namespace Features.Items.StaticData
{
	[CreateAssetMenu(menuName = Menu.StaticData + nameof(CubesConfigsCollection))]
	public sealed class CubesConfigsCollection : ScriptableObject
	{
		public CubeConfig[] Configs;
	}
}