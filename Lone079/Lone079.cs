namespace Lone079
{
    using System;
    using Exiled.API.Features;

    public class Lone079 : Plugin<Config>
    {
        private EventHandlers eventHandlers;

        public override Version RequiredExiledVersion { get; } = new Version(5, 0, 0);

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