using System.Collections.Generic;
using MEC;
using Smod2;
using Smod2.API;
using Smod2.Attributes;
using Smod2.Config;

namespace RainbowTag
{
	[PluginDetails(
		name = "Rainbow Tag",
		id = "sirmeepington.rainbowtag",
		author = "SirMeepington",
		configPrefix = "rt",
		description = "Allows admins to set a user rank to have a rainbow colored tag.",
		SmodMajor = 3,
		SmodMinor = 4,
		SmodRevision = 0,
		version = "0.1"
		)]
	public class RainbowTag : Plugin
	{
		
		private List<string> _rainbowIDs = new List<string>();

		private readonly DataFileManager _dataFileManager = new DataFileManager();
		
		public static readonly List<string> AvailableColors = new List<string>()
		{
			"pink","red","brown","silver","light_green","crimson","cyan","aqua","deep_pink","tomato","yellow","magenta",
			"blue_green","orange","lime","green","emerald","carmine","nickel","mint","army_green","pumpkin","default"
		};

		private CoroutineHandle _handle;

		private RainbowHandler _rainbowHandler;

		public readonly Dictionary<string, ServerRoles> rainbowInfo = new Dictionary<string, ServerRoles>();

		[ConfigOption()]
		public readonly float timerDelay = 0.1f; // rt_timer_delay: 0.1
		
		public override void Register()
		{
			AddCommand("reloadrainbow", new ReloadCommand(this));
			_rainbowHandler = new RainbowHandler(this);
			AddEventHandlers(new ReloadEvents(this));
			AddEventHandlers(new PlayerEvents(this));
		}

		public override void OnEnable() {}
	
		public override void OnDisable() {}

		public void Reload()
		{
			Info("Reloading rainbow tags...");
			
			_rainbowIDs = _dataFileManager.ReadFile();
			rainbowInfo.Clear();
			foreach (string id in _rainbowIDs)
			{
				if (rainbowInfo.ContainsKey(id))
					continue;

				rainbowInfo.Add(id,null);
			}

			foreach (Player p in Server.GetPlayers())
			{
				if (rainbowInfo.ContainsKey(p.SteamId) && rainbowInfo[p.SteamId] == null)
					rainbowInfo[p.SteamId] = PlayerEvents.GetRolesFromPlayer(p);
			}
			
			if (_handle.IsRunning)
				Timing.KillCoroutines(_handle);

			RainbowHandler.canRun = true;
			_handle = Timing.RunCoroutine(_rainbowHandler.RainbowTick());

			Info("Reload completed.");
		}
	}
}