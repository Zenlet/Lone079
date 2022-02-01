using Exiled.API.Interfaces;
using System.Collections.Generic;

namespace Lone079
{
	public class Config : IConfig
	{
		public bool IsEnabled { get; set; } = true;

		public bool CountZombies { get; set; } = false;

		public int HealthPercent { get; set; } = 50;
		public string BroadcastMessage { get; set; } = "<i>You have been respawned as a random SCP with half health because all other SCPs have died.</i>";
		public ushort BroadcastTime { get; set; } = 10;

		public List<RoleType> scp079Respawns { get; set; } = new List<RoleType>
		{
			RoleType.Scp173,
			RoleType.Scp049,
			RoleType.Scp096,
			RoleType.Scp106,
			RoleType.Scp93953,
			RoleType.Scp93989
		};

		public List<RoleType> scp079RespawnLocations { get; set; } = new List<RoleType>
		{
			RoleType.Scp049,
			RoleType.Scp096,
			RoleType.Scp93953
		};
	}
}