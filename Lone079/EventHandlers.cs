using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Exiled.API.Extensions;

namespace Lone079
{
	class EventHandlers
	{
		private Vector3 scppos;
		private bool is106Contained, canChange079;
		public void Check079()
		{
			if (Map.ActivatedGenerators != 3 && canChange079 == true)
			{
				List<Player> scp = new List<Player>();
				foreach (Player player in Player.List)
				{
					if (player.Team == Team.SCP)
					{
						scp.Add(player);
					}
				}
				
				if (Lone079.plugin.Config.CountZombies == false)
				{
					scp.RemoveAll(x => x.Role == RoleType.Scp0492);
				}
				List<Player> pList = scp.ToList();
				if (pList.Count == 1 && pList[0].Role == RoleType.Scp079)
				{
					Player player = pList[0];
					RoleType role = Lone079.plugin.Config.scp079Respawns[Random.Range(0, Lone079.plugin.Config.scp079Respawns.Count)];
					if (is106Contained && role == RoleType.Scp106)
					{
						role = Lone079.plugin.Config.scp079RespawnLocations[Random.Range(0, Lone079.plugin.Config.scp079RespawnLocations.Count)];
					}
					player.SetRole(role);
					Timing.CallDelayed(1f, () => player.Position = scppos);
					player.Health = (int)(player.MaxHealth * (Lone079.plugin.Config.HealthPercent / 100f));
					player.Broadcast(Lone079.plugin.Config.BroadcastTime, Lone079.plugin.Config.BroadcastMessage);
				}
			}
		}

		public void OnPlayerLeave(LeftEventArgs ev)
		{
			if (ev.Player.Team == Team.SCP)
			{
				Check079();
			}
		}

		public void OnDetonated()
		{
			canChange079 = false;
		}

		public void OnRoundStart()
		{
			scppos = Lone079.plugin.Config.scp079RespawnLocations[Random.Range(0, Lone079.plugin.Config.scp079RespawnLocations.Count)].GetRandomSpawnProperties().Item1;
			is106Contained = false;
			canChange079 = true;
		}

		public void OnPlayerDied(DiedEventArgs ev)
		{
			if (ev.Handler.Attacker == null || !RoundSummary.RoundInProgress())
			{
				return;
			}

			if (ev.Handler.Attacker.Team == Team.SCP)
			{
				Check079();
			}
		}

		public void OnScp106Contain(ContainingEventArgs ev)
		{
			is106Contained = true;
		}
	}
}