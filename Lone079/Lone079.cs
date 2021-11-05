using Exiled.API.Features;
using System;

namespace Lone079
{
	public class Lone079 : Plugin<Config>
	{
		public override string Name { get; } = "Lone079";
		public override string Author { get; } = "Zenlet";
		public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);
		public override Version Version { get; } = new Version(1, 2, 2);

		public static Lone079 instance;
		private EventHandlers ev;

		public override void OnEnabled()
		{
			instance = this;
			ev = new EventHandlers();

			Exiled.Events.Handlers.Server.RoundStarted += ev.OnRoundStart;
			Exiled.Events.Handlers.Player.Died += ev.OnPlayerDied;
			Exiled.Events.Handlers.Player.Left += ev.OnPlayerLeave;
			Exiled.Events.Handlers.Scp106.Containing += ev.OnScp106Contain;
			Exiled.Events.Handlers.Warhead.Detonated += ev.OnDetonated;
			base.OnEnabled();
		}

		public override void OnDisabled()
		{
			Exiled.Events.Handlers.Server.RoundStarted -= ev.OnRoundStart;
			Exiled.Events.Handlers.Player.Died -= ev.OnPlayerDied;
			Exiled.Events.Handlers.Player.Left -= ev.OnPlayerLeave;
			Exiled.Events.Handlers.Scp106.Containing -= ev.OnScp106Contain;
			Exiled.Events.Handlers.Warhead.Detonated -= ev.OnDetonated;
			ev = null;
			base.OnDisabled();
		}
	}
}
