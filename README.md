# Lone079

SCP-079 will be turned into a random SCP with partial health and spawned in either 939, 049 or 096's containment chamber when he is the last SCP alive instead of being auto recontained.

# Installation

**[EXILED](https://github.com/galaxy119/EXILED) must be installed for this to work.**

Place the "Lone079.dll" file in your EXILED/Plugins folder.

# Configs

| Config        | Value Type | Default Value | Description |
| :-------------: | :---------: | :------: | :--------- |
| 079_count_zombies | Boolean | False | Determines if zombies should be counted when determining if SCP-079 is the last SCP alive. |
| 079_health_percentage | Integer | 50 | The percentage of normal health SCP-079 should have when he respawns as a random SCP. |
| Broadcast_Message | String | <i>You have been respawned as a random SCP with half health because all other SCPs have died.</i> | The message shown to the player when they have spawned in as a different scp. |
| BroadcastTime | Integer | 10 | Broadcast_Message time |
| scp079_respawns | List |  | Determines what SCP079 will be respawned as after the last scp dies. |
| scp079_respawn_locations | List |  | Determines where scp079 will be respawned. Only accepts scp names. |
