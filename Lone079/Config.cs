namespace Lone079
{
    using System.Collections.Generic;
	using System.ComponentModel;
	using Exiled.API.Features;
    using Exiled.API.Interfaces;

    public class Config : IConfig
    {
        [Description("Enables or disables the plugin")]
        public bool IsEnabled { get; set; } = true;
        [Description("Determines if zombies should be counted when determining if SCP-079 is the last SCP alive.")]
        public bool CountZombies { get; set; } = false;
        [Description("The percentage of normal health SCP-079 should have when he respawns as a random SCP.")]
        public int HealthPercent { get; set; } = 50;
        [Description("The message shown to the player when they have spawned in as a different scp.")]
        public Broadcast Broadcast { get; set; } = new Broadcast("<i>You have been respawned as a random SCP with half health because all other SCPs have died.</i>");
        [Description("Determines what SCP079 will be respawned as after the last scp dies.")]
        public List<RoleType> Scp079Respawns { get; set; } = new List<RoleType>
        {
            RoleType.Scp173,
            RoleType.Scp049,
            RoleType.Scp096,
            RoleType.Scp106,
            RoleType.Scp93953,
            RoleType.Scp93989,
        };
        [Description("Determines where scp079 will be respawned. Only accepts scp names.")]
        public List<RoleType> Scp079RespawnLocations { get; set; } = new List<RoleType>
        {
            RoleType.Scp049,
            RoleType.Scp096,
            RoleType.Scp93953,
        };
        public bool Validate(out string error)
        {
            if (Scp079Respawns == null || Scp079Respawns.Count == 0)
            {
                error = "Scp079Respawns config could not be validated. Ensure you have at least one entry.";
                return false;
            }

            if (Scp079RespawnLocations == null || Scp079RespawnLocations.Count == 0)
            {
                error = "Scp079RespawnLocations config could not be validated. Ensure you have at least one entry.";
                return false;
            }

            error = null;
            return true;
        }
    }
}