using Exiled.API.Features;
using System;

namespace Lone079
{
	public class Lone079 : Plugin<Config>
	{
		public override string Name { get; } = "Lone079";
		public override string Author { get; } = "Zenlet";
		public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);
		public override Version Version { get; } = new Version(1, 2, 1);

		public static Lone079 instance;
		private EventHandlers ev;

		public override void OnEnabled()
		{
			base.OnEnabled();
			instance = this;
			ev = new EventHandlers();
			if (!Config.IsEnabled) return;

			Exiled.Events.Handlers.Server.RoundStarted += ev.OnRoundStart;
			Exiled.Events.Handlers.Player.Died += ev.OnPlayerDied;
			Exiled.Events.Handlers.Player.Left += ev.OnPlayerLeave;
			Exiled.Events.Handlers.Scp106.Containing += ev.OnScp106Contain;
			Exiled.Events.Handlers.Warhead.Detonated += ev.OnDetonated;
		}

		public override void OnDisabled()
		{
			base.OnDisabled();

			Exiled.Events.Handlers.Server.RoundStarted -= ev.OnRoundStart;
			Exiled.Events.Handlers.Player.Died -= ev.OnPlayerDied;
			Exiled.Events.Handlers.Player.Left -= ev.OnPlayerLeave;
			Exiled.Events.Handlers.Scp106.Containing -= ev.OnScp106Contain;
			Exiled.Events.Handlers.Warhead.Detonated -= ev.OnDetonated;

			ev = null;
		}
	}
}
