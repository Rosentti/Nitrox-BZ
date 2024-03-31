using NitroxClient_BelowZero.GameLogic;
using NitroxModel.Core;
using UnityEngine;

namespace NitroxClient_BelowZero.MonoBehaviours
{
    public class PlayerDeathBroadcaster : MonoBehaviour
    {
        private LocalPlayer localPlayer;

        public void Awake()
        {
            localPlayer = NitroxServiceLocator.LocateService<LocalPlayer>();

            Player.main.playerDeathEvent.AddHandler(this, PlayerDeath);
        }

        private void PlayerDeath(Player player)
        {
            localPlayer.BroadcastDeath(player.transform.position);
        }
    }
}
