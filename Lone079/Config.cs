using Exiled.API.Interfaces;

namespace Lone079
{
	public class Config : IConfig
	{
		public bool IsEnabled { get; set; } = true;

		public bool CountZombies { get; set; } = false;
		public bool ScaleWithLevel { get; set; } = false;

		public int HealthPercent { get; set; } = 50;
		public string Broadcast_Message { get; set; } = "<i>You have been respawned as a random SCP with half health because all other SCPs have died.</i>";
		public int BroadcastTime { get; set; } = 10;
	}
}
