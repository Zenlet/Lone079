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

		private IEnumerator<float> Check079(float delay = 1f)
		{
			if (Map.ActivatedGenerators != 3 && canChange079 == true)
			{
				yield return Timing.WaitForSeconds(delay);
				IEnumerable<Player> enumerable = Player.Get(Team.SCP);
				if (!Lone079.instance.Config.CountZombies) enumerable = enumerable.Where(x => x.Role != RoleType.Scp0492);
				List< Player> pList = enumerable.ToList();
				if (pList.Count == 1 && pList[0].Role == RoleType.Scp079)
				{
					Player player = pList[0];
					int level = player.Level;
					RoleType role = Lone079.instance.Config.scp079Respawns[Random.Range(0, Lone079.instance.Config.scp079Respawns.Count)];
					if (is106Contained && role == RoleType.Scp106) role = RoleType.Scp93953;
					player.SetRole(role);
					Timing.CallDelayed(1f, () => player.Position = scppos);
					player.Health = !Lone079.instance.Config.ScaleWithLevel ? player.MaxHealth * (Lone079.instance.Config.HealthPercent / 100f) : player.MaxHealth * ((Lone079.instance.Config.HealthPercent + ((level - 1) * 5)) / 100f);
					player.Broadcast(Lone079.instance.Config.BroadcastTime, Lone079.instance.Config.BroadcastMessage);
				}
			}
		}

        public void OnPlayerLeave(LeftEventArgs ev)
		{
			if (ev.Player.Team == Team.SCP) Timing.RunCoroutine(Check079(3f));
		}

		public void OnDetonated() => canChange079 = false;

		public void OnRoundStart()
		{
			scppos = Lone079.instance.Config.scp079RespawnLocations[Random.Range(0, Lone079.instance.Config.scp079RespawnLocations.Count)].GetRandomSpawnProperties().Item1;
			is106Contained = false;
			canChange079 = true;
		}

		public void OnPlayerDied(DiedEventArgs ev)
		{
			if (ev.Target.Team == Team.SCP) Timing.RunCoroutine(Check079(3f));
		}

		public void OnScp106Contain(ContainingEventArgs ev)
		{
			is106Contained = true;
		}
	}
}
