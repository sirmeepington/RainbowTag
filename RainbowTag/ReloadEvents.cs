using Smod2.EventHandlers;
using Smod2.Events;

namespace RainbowTag
{
	public class ReloadEvents : IEventHandlerWaitingForPlayers, IEventHandlerRoundRestart, IEventHandlerRoundStart
	{

		private readonly RainbowTag _plugin;

		public ReloadEvents(RainbowTag plugin) => _plugin = plugin;

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev) => _plugin.Reload();	

		public void OnRoundRestart(RoundRestartEvent ev) => RainbowHandler.canRun = false;

		public void OnRoundStart(RoundStartEvent ev) => _plugin.Reload();
	}
}