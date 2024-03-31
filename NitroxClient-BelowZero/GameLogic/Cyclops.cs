using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NitroxClient_BelowZero.Communication;
using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel_BelowZero.DataStructures;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.Util;
using NitroxModel.Packets;
using NitroxModel_BelowZero.DataStructures.GameLogic;
using NitroxModel_BelowZero.Packets;
using UnityEngine;
using static NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor.CyclopsMetadataExtractor;

namespace NitroxClient_BelowZero.GameLogic
{
    public class Cyclops
    {
        private readonly IPacketSender packetSender;
        private readonly Vehicles vehicles;
        private readonly Entities entities;

        public Cyclops(IPacketSender packetSender, Vehicles vehicles, Entities entities)
        {
            this.packetSender = packetSender;
            this.vehicles = vehicles;
            this.entities = entities;
        }

        public void BroadcastMetadataChange(NitroxId id)
        {
            GameObject gameObject = NitroxEntity.RequireObjectFrom(id);
            CyclopsGameObject cyclops = new CyclopsGameObject() { GameObject = gameObject };
            entities.EntityMetadataChanged(cyclops, id);
        }

        public void BroadcastLaunchDecoy(NitroxId id)
        {
            CyclopsDecoyLaunch packet = new CyclopsDecoyLaunch(id);
            packetSender.Send(packet);
        }

        public void BroadcastActivateFireSuppression(NitroxId id)
        {
            CyclopsFireSuppression packet = new CyclopsFireSuppression(id);
            packetSender.Send(packet);
        }

        public void LaunchDecoy(NitroxId id)
        {
            GameObject cyclops = NitroxEntity.RequireObjectFrom(id);
            CyclopsDecoyManager decoyManager = cyclops.RequireComponent<CyclopsDecoyManager>();
            using (PacketSuppressor<EntityMetadataUpdate>.Suppress())
            {
                decoyManager.Invoke(nameof(CyclopsDecoyManager.LaunchWithDelay), 3f);
                decoyManager.decoyLaunchButton.UpdateText();
                decoyManager.subRoot.voiceNotificationManager.PlayVoiceNotification(decoyManager.subRoot.decoyNotification, false, true);
                decoyManager.subRoot.BroadcastMessage("UpdateTotalDecoys", decoyManager.decoyCount, SendMessageOptions.DontRequireReceiver);
                CyclopsDecoyLaunchButton decoyLaunchButton = cyclops.RequireComponentInChildren<CyclopsDecoyLaunchButton>();
                decoyLaunchButton.StartCooldown();
            }
        }

        public void StartFireSuppression(NitroxId id)
        {
            GameObject cyclops = NitroxEntity.RequireObjectFrom(id);
            CyclopsFireSuppressionSystemButton fireSuppButton = cyclops.RequireComponentInChildren<CyclopsFireSuppressionSystemButton>();
            using (PacketSuppressor<CyclopsFireSuppression>.Suppress())
            {
                // Infos from SubFire.StartSystem
                fireSuppButton.subFire.StartCoroutine(StartFireSuppressionSystem(fireSuppButton.subFire));
                fireSuppButton.StartCooldown();
            }
        }

        // Remake of the StartSystem Coroutine from original player. Some Methods are not used from the original coroutine
        // For example no temporaryClose as this will be initiated anyway from the originating Player
        // Also the fire extiguishing will not start cause the initial player is already extiguishing the fires. Else this could double/triple/... the extinguishing
        private IEnumerator StartFireSuppressionSystem(SubFire fire)
        {
            fire.subRoot.voiceNotificationManager.PlayVoiceNotification(fire.subRoot.fireSupressionNotification, false, true);
            yield return Yielders.WaitFor3Seconds;
            fire.fireSuppressionActive = true;
            fire.subRoot.fireSuppressionState = true;
            fire.subRoot.BroadcastMessage("NewAlarmState", null, SendMessageOptions.DontRequireReceiver);
            fire.Invoke(nameof(SubFire.CancelFireSuppression), fire.fireSuppressionSystemDuration);
            float doorCloseDuration = 30f;
            fire.gameObject.BroadcastMessage("TemporaryLock", doorCloseDuration, SendMessageOptions.DontRequireReceiver);
        }

        /// <summary>
        /// Triggers a <see cref="CyclopsDamage"/> packet
        /// </summary>
        public void OnCreateDamagePoint(SubRoot subRoot)
        {
            BroadcastDamageState(subRoot, Optional.Empty);
        }

        /// <summary>
        /// Called when the player repairs a <see cref="CyclopsDamagePoint"/>. Right now it's not possible to partially repair because it would be difficult to implement.
        /// <see cref="CyclopsDamagePoint"/>s are coupled with <see cref="LiveMixin"/>, which is used with just about anything that has health.
        /// I would need to hook onto <see cref="LiveMixin.AddHealth(float)"/>, or maybe the repair gun event to catch when something repairs a damage point, which I don't
        /// believe is worth the effort. A <see cref="CyclopsDamagePoint"/> is already fully repaired in a little over a second. This can trigger sending
        /// <see cref="CyclopsDamagePointRepaired"/> and <see cref="CyclopsDamage"/> packets
        /// </summary>
        public void OnDamagePointRepaired(SubRoot subRoot, CyclopsDamagePoint damagePoint, float repairAmount)
        {
            if (!subRoot.TryGetIdOrWarn(out NitroxId subId))
            {
                return;
            }

            for (int i = 0; i < subRoot.damageManager.damagePoints.Length; i++)
            {
                if (subRoot.damageManager.damagePoints[i] == damagePoint)
                {
                    CyclopsDamagePointRepaired packet = new(subId, i, repairAmount);
                    packetSender.Send(packet);

                    return;
                }
            }
        }

        /// <summary>
        /// Send out a <see cref="CyclopsDamage"/> packet
        /// </summary>
        private void BroadcastDamageState(SubRoot subRoot, Optional<DamageInfo> info)
        {
            if (!subRoot.TryGetIdOrWarn(out NitroxId subId))
            {
                return;
            }

            LiveMixin subHealth = subRoot.gameObject.RequireComponent<LiveMixin>();
            if (subHealth.health <= 0)
            {
                return;
            }
            CyclopsDamageInfoData damageInfo = null;
            if (info.HasValue)
            {
                DamageInfo damage = info.Value;
                Optional<NitroxId> dealerId = damage.dealer.GetId();
                // Source of the damage. Used if the damage done to the Cyclops was not calculated on other clients. Currently it's just used to figure out what sounds and
                // visual effects should be used.
                damageInfo = new CyclopsDamageInfoData(subId, dealerId, damage.originalDamage, damage.damage, damage.position.ToDto(), damage.type);
            }

            int[] damagePointIndexes = GetActiveDamagePoints(subRoot).ToArray();
            CyclopsFireData[] firePoints = GetActiveRoomFires(subRoot.GetComponent<SubFire>()).ToArray();

            CyclopsDamage packet = new(subId, subRoot.GetComponent<LiveMixin>().health, subRoot.damageManager.subLiveMixin.health, subRoot.GetComponent<SubFire>().liveMixin.health, damagePointIndexes, firePoints, damageInfo);
            packetSender.Send(packet);
        }

        /// <summary>
        /// Get all of the index locations of <see cref="CyclopsDamagePoint"/>s in <see cref="CyclopsExternalDamageManager.damagePoints"/>.
        /// </summary>
        private IEnumerable<int> GetActiveDamagePoints(SubRoot subRoot)
        {
            for (int i = 0; i < subRoot.damageManager.damagePoints.Length; i++)
            {
                if (subRoot.damageManager.damagePoints[i].gameObject.activeSelf)
                {
                    yield return i;
                }
            }
        }

        /// <summary>
        /// Get all of the index locations of all the fires on the <see cref="SubRoot"/>. <see cref="SubFire.RoomFire.spawnNodes"/> contains
        /// a static list of all possible fire nodes.
        /// </summary>
        private IEnumerable<CyclopsFireData> GetActiveRoomFires(SubFire subFire)
        {
            if (!subFire.subRoot.TryGetIdOrWarn(out NitroxId subRootId))
            {
                yield break;
            }

            foreach (KeyValuePair<CyclopsRooms, SubFire.RoomFire> roomFire in subFire.roomFires)
            {
                for (int i = 0; i < roomFire.Value.spawnNodes.Length; i++)
                {
                    if (roomFire.Value.spawnNodes[i].childCount > 0)
                    {
                        if (!roomFire.Value.spawnNodes[i].GetComponentInChildren<Fire>().TryGetIdOrWarn(out NitroxId fireId))
                        {
                            yield break;
                        }

                        yield return new CyclopsFireData(fireId, subRootId, roomFire.Key, i);
                    }
                }
            }
        }
    }
}
