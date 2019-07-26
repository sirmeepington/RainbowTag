using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using UnityEngine;

namespace RainbowTag
{
	public class PlayerEvents : IEventHandlerPlayerJoin
	{
		private readonly RainbowTag _plugin;

		public PlayerEvents(RainbowTag plugin) => _plugin = plugin;
		
		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (_plugin.rainbowInfo.ContainsKey(ev.Player.SteamId) && _plugin.rainbowInfo[ev.Player.SteamId] == null)
				_plugin.rainbowInfo[ev.Player.SteamId] = GetRolesFromPlayer(ev.Player);
			
		}

		public static ServerRoles GetRolesFromPlayer(Player p)
			=>  ((GameObject) p?.GetGameObject())?.GetComponent<ServerRoles>();
	}
}