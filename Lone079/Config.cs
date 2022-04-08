namespace Lone079
{
    using System.Collections.Generic;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;

    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool CountZombies { get; set; } = false;
        public int HealthPercent { get; set; } = 50;
        public Broadcast Broadcast { get; set; } = new Broadcast("<i>You have been respawned as a random SCP with half health because all other SCPs have died.</i>");
        public List<RoleType> Scp079Respawns { get; set; } = new List<RoleType>
        {
            RoleType.Scp173,
            RoleType.Scp049,
            RoleType.Scp096,
            RoleType.Scp106,
            RoleType.Scp93953,
            RoleType.Scp93989,
        };
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