using System.Collections.Generic;
using MEC;
using Random = System.Random;

namespace RainbowTag
{
	
	public class RainbowHandler
	{

		private readonly RainbowTag _plugin;

		public static bool canRun = false;

		public RainbowHandler(RainbowTag plugin) => _plugin = plugin;

		private string _currentRainbow = "";

		private readonly Random _random = new Random();

		public IEnumerator<float> RainbowTick()
		{
			while (canRun)
			{
				foreach (ServerRoles roles in _plugin.rainbowInfo.Values)
				{
					if (roles == null)
						continue;
					
					if (!string.IsNullOrEmpty(roles.MyText)) // avoid hidden roles. might work depending on how SCP:SL defines a hidden role
						roles.NetworkMyColor = _currentRainbow;
				}

				NewColor();
				yield return Timing.WaitForSeconds(_plugin.timerDelay);
			}
		}

		private void NewColor() 
			=> _currentRainbow = RainbowTag.AvailableColors[_random.Next(RainbowTag.AvailableColors.Count)];

	}
}