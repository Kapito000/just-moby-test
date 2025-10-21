using Infrastructure;

namespace Features.Input
{
	public interface IInputService : IService
	{
		void Enable();
		void Disable();
	}
}