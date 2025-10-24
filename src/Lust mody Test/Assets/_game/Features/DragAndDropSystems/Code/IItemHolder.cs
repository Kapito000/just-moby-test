namespace Features.DragAndDropSystems
{
	public interface IItemHolder
	{
		void Hold(bool value);
		public bool IsHold { get; }
	}
}