using NitroxClient_BelowZero.GameLogic;
using NitroxClient_BelowZero.Unity.Helper;
using UnityEngine;

namespace NitroxClient_BelowZero.MonoBehaviours;

public class PlayerStatsBroadcaster : MonoBehaviour
{
    private float time;
    private const float BROADCAST_INTERVAL = 3f;
    private LocalPlayer localPlayer;
    private Survival survival;

    public void Awake()
    {
        localPlayer = this.Resolve<LocalPlayer>();
        survival = Player.main.AliveOrNull()?.GetComponent<Survival>();
        if (!survival)
        {
            Log.Error($"Couldn't find the {nameof(Survival)} instance on the main {nameof(Player)} instance. Destroying {nameof(PlayerStatsBroadcaster)}");
            Destroy(this);
        }
    }

    public void Update()
    {
        time += Time.deltaTime;

        // Only do on a specific cadence to avoid hammering server
        if (time >= BROADCAST_INTERVAL)
        {
            time = 0;

            //TODOBZ
            float oxygen = Player.main.oxygenMgr.GetOxygenAvailable();
            float maxOxygen = Player.main.oxygenMgr.GetOxygenCapacity();
            float health = Player.main.liveMixin.health;
            float food = survival.food;
            float water = survival.water;
            localPlayer.BroadcastStats(oxygen, maxOxygen, health, food, water, 0, survival.bodyTemperature.currentColdMeterValue);
        }
    }
}
