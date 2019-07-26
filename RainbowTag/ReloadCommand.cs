using Smod2.Commands;

namespace RainbowTag
{
	public class ReloadCommand : ICommandHandler
	{
		private readonly RainbowTag _plugin;
		
		public ReloadCommand (RainbowTag plugin) => _plugin = plugin;
		
		public string[] OnCall(ICommandSender sender, string[] args)
		{
			RainbowHandler.canRun = false;
			_plugin.Reload();
			return new [] {"Reloading rainbow ID list..."};
		}

		public string GetUsage() => "RELOADRAINBOW";

		public string GetCommandDescription() => "Reloads the rainbow ID list.";
	}
}