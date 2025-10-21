using UnityEngine;
using Menu = Constants.CreateAssetMenu;

namespace Features.Cubes.StaticData
{
	[CreateAssetMenu(menuName = Menu.StaticData + nameof(CubesConfigsCollection))]
	public sealed class CubesConfigsCollection : ScriptableObject
	{
		public CubeConfig[] Configs;
	}
}