// -----------------------------------------------------------------------
// <copyright file="EventHandlers.cs" company="Zenlet">
// Copyright (c) Zenlet. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lone079
{
    using System.Collections.Generic;
    using System.Linq;
    using Exiled.API.Enums;
    using Exiled.API.Extensions;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using MEC;
    using NorthwoodLib.Pools;
    using UnityEngine;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers"/>.
    /// </summary>
    public class EventHandlers
    {
        private readonly Lone079 lone079;
        private bool canChange079;
        private bool is106Contained;
        private Vector3 scpPos;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="lone079">An instance of the <see cref="Lone079"/> class.</param>
        public EventHandlers(Lone079 lone079) => this.lone079 = lone079;

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnDied(DiedEventArgs)"/>
        public void OnDied(DiedEventArgs ev)
        {
            if (ev.TargetOldRole.GetTeam() == Team.SCP)
                Check079();
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Scp106.OnContaining(ContainingEventArgs)"/>
        public void OnContaining(ContainingEventArgs ev) => is106Contained = true;

        /// <inheritdoc cref="Exiled.Events.Handlers.Server.OnRoundStarted"/>
        public void OnRoundStarted()
        {
            scpPos = lone079.Config.Scp079RespawnLocations[Random.Range(0, lone079.Config.Scp079RespawnLocations.Count)].GetRandomSpawnProperties().Item1;
            is106Contained = false;
            canChange079 = true;
        }

        /// <inheritdoc cref="Exiled.Events.Handlers.Warhead.OnDetonated"/>
        public void OnDetonated()
        {
            canChange079 = false;
        }

        private void Check079()
        {
            if (Generator.Get(GeneratorState.Engaged).Count() == 3 || !canChange079)
                return;

            List<Player> scpList = lone079.Config.CountZombies
                ? Player.Get(Team.SCP).ToList()
                : Player.Get(player => player.IsScp && player.Role.Type != RoleType.Scp0492).ToList();

            if (scpList.Count == 1 && scpList[0].Role == RoleType.Scp079)
            {
                Player player = scpList[0];
                if (!TryGetRole(out RoleType roleType))
                    return;

                player.SetRole(roleType);
                player.Health = player.MaxHealth * (lone079.Config.HealthPercent / 100f);
                player.Broadcast(lone079.Config.Broadcast);
                Timing.CallDelayed(1f, () => player.Position = scpPos);
            }
        }

        private bool TryGetRole(out RoleType roleType)
        {
            List<RoleType> availableRoles = ListPool<RoleType>.Shared.Rent(lone079.Config.Scp079Respawns);
            if (is106Contained)
                availableRoles.RemoveAll(role => role == RoleType.Scp106);

            if (availableRoles.Count == 0)
            {
                roleType = RoleType.None;
                ListPool<RoleType>.Shared.Return(availableRoles);
                return false;
            }

            roleType = availableRoles[Random.Range(0, availableRoles.Count)];
            ListPool<RoleType>.Shared.Return(availableRoles);
            return true;
        }
    }
}