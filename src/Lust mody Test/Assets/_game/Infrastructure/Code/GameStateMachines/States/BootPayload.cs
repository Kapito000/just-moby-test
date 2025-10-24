using UI;

namespace Infrastructure.GameStateMachines.States
{
	public struct BootPayload
	{
		public IMainMediator MainMediator;
		public IBootEnter[] BootEnters;
	}
}