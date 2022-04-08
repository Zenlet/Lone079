// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Zenlet">
// Copyright (c) Zenlet. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lone079
{
    using System.Collections.Generic;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public class Config : IConfig
    {
        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="RoleType.Scp0492"/> instances should be counted as an SCP.
        /// </summary>
        public bool CountZombies { get; set; } = false;

        /// <summary>
        /// Gets or sets the percentage of health <see cref="RoleType.Scp079"/> should spawn in with.
        /// </summary>
        public int HealthPercent { get; set; } = 50;

        /// <summary>
        /// Gets or sets the broadcast to send to the <see cref="RoleType.Scp079"/> when they get respawned as a new Scp.
        /// </summary>
        public Broadcast Broadcast { get; set; } = new Broadcast("<i>You have been respawned as a random SCP with half health because all other SCPs have died.</i>");

        /// <summary>
        /// Gets or sets the roles that the <see cref="RoleType.Scp079"/> can spawn as.
        /// </summary>
        public List<RoleType> Scp079Respawns { get; set; } = new List<RoleType>
        {
            RoleType.Scp173,
            RoleType.Scp049,
            RoleType.Scp096,
            RoleType.Scp106,
            RoleType.Scp93953,
            RoleType.Scp93989,
        };

        /// <summary>
        /// Gets or sets the spawn locations that <see cref="RoleType.Scp079"/> can respawn at.
        /// </summary>
        public List<RoleType> Scp079RespawnLocations { get; set; } = new List<RoleType>
        {
            RoleType.Scp049,
            RoleType.Scp096,
            RoleType.Scp93953,
        };

        /// <summary>
        /// Validates that <see cref="Scp079Respawns"/> and <see cref="Scp079RespawnLocations"/> have entries.
        /// </summary>
        /// <param name="error">The error, or null if there isn't one.</param>
        /// <returns>A value indicating whether the config is valid.</returns>
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