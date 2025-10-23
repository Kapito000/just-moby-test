using Features.Cubes;

namespace Features.DragAndDropCubes
{
	public interface IPlaceCondition
	{
		bool CanPlace(IGameCube cube);
	}
}