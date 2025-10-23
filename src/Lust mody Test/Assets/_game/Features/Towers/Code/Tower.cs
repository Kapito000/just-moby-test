using System.Collections.Generic;
using Features.Cubes;
using Features.DragAndDropCubes;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Features.Towers
{
	public sealed class Tower : MonoBehaviour, IDropTarget
	{
		[ReadOnly]
		[SerializeField] List<CubePlacement> _placements;

		IPlaceCondition[] _placeConditions;

		[Inject]
		void Construct(IPlaceCondition[] placeConditions)
		{
			_placeConditions = placeConditions;
		}
		
		public void Place(IGameCube cube)
		{
			if (CanPlace(cube) == false)
			{
				cube.Destroy();
				return;
			}
		}

		bool CanPlace(IGameCube cube)
		{
			foreach (var condition in _placeConditions)
			{
				if (condition.CanPlace(cube) == false)
					return false;
			}

			return true;
		}
	}
}