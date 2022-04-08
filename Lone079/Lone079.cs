// -----------------------------------------------------------------------
// <copyright file="Lone079.cs" company="Zenlet">
// Copyright (c) Zenlet. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Lone079
{
    using System;
    using Exiled.API.Features;

    /// <summary>
    /// The main plugin class.
    /// </summary>
    public class Lone079 : Plugin<Config>
    {
        private EventHandlers eventHandlers;

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new Version(5, 0, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            if (!Config.Validate(out string error))
            {
                Log.Error(error);
                return;
            }

            eventHandlers = new EventHandlers(this);
            Exiled.Events.Handlers.Player.Died += eventHandlers.OnDied;
            Exiled.Events.Handlers.Scp106.Containing += eventHandlers.OnContaining;
            Exiled.Events.Handlers.Server.RoundStarted += eventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Warhead.Detonated += eventHandlers.OnDetonated;
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Died -= eventHandlers.OnDied;
            Exiled.Events.Handlers.Scp106.Containing -= eventHandlers.OnContaining;
            Exiled.Events.Handlers.Server.RoundStarted -= eventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Warhead.Detonated -= eventHandlers.OnDetonated;
            eventHandlers = null;
            base.OnDisabled();
        }
    }
}