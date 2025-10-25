namespace Features.DragAndDropSystems
{
	public interface IItemHolder
	{
		bool IsHold { get; }
		void Hold(bool value);

		void Accept(IItemHolderVisitor visitor);
	}
}