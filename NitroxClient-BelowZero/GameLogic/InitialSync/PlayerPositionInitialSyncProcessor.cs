using System.Collections;
using NitroxClient_BelowZero.Communication;
using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.GameLogic.InitialSync.Abstract;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.Util;
using NitroxModel.Packets;
using NitroxModel_BelowZero.DataStructures;
using UnityEngine;
using Math = System.Math;

namespace NitroxClient_BelowZero.GameLogic.InitialSync;

public class PlayerPositionInitialSyncProcessor : InitialSyncProcessor
{
    private static readonly Vector3 spawnRelativeToEscapePod = new Vector3(0.9f, 2.1f, 0);

    private readonly IPacketSender packetSender;

    public PlayerPositionInitialSyncProcessor(IPacketSender packetSender)
    {
        this.packetSender = packetSender;

        AddDependency<PlayerInitialSyncProcessor>();
        AddDependency<GlobalRootInitialSyncProcessor>();
    }

    public override IEnumerator Process(InitialPlayerSync packet, WaitScreen.ManualWaitItem waitScreenItem)
    {
        // We freeze the player so that he doesn't fall before the cells around him have loaded
        Player.main.cinematicModeActive = true;

        AttachPlayerToEscapePod(packet.AssignedEscapePodId);

        Vector3 position = packet.PlayerSpawnData.ToUnity();
        Quaternion rotation = packet.PlayerSpawnRotation.ToUnity();
        if (Math.Abs(position.x) < 0.0002 && Math.Abs(position.y) < 0.0002 && Math.Abs(position.z) < 0.0002)
        {
            position = Player.mainObject.transform.position;
        }
        Player.main.SetPosition(position, rotation);

        // Player position is relative to a subroot if in a subroot
        Optional<NitroxId> subRootId = packet.PlayerSubRootId;
        if (!subRootId.HasValue)
        {
            yield return Terrain.WaitForWorldLoad();
            yield break;
        }

        Optional<GameObject> sub = NitroxEntity.GetObjectFrom(subRootId.Value);
        if (!sub.HasValue)
        {
            Log.Error($"Could not spawn player into subroot with id: {subRootId.Value}");
            yield return Terrain.WaitForWorldLoad();
            yield break;
        }

        if (!sub.Value.TryGetComponent(out SubRoot subRoot))
        {
            Log.Debug("SubRootId-GameObject has no SubRoot component, so it's assumed to be the EscapePod");
            yield return Terrain.WaitForWorldLoad();
            yield break;
        }

        Player.main.SetCurrentSub(subRoot);
        if (subRoot.isBase)
        {
            // If the player's in a base, we don't need to wait for the world to load
            Player.main.cinematicModeActive = false;
            yield break;
        }

        Transform rootTransform = subRoot.transform;
        Quaternion vehicleAngle = rootTransform.rotation;
        // "position" is a relative position and "positionInVehicle" an absolute position
        Vector3 positionInVehicle = vehicleAngle * position + rootTransform.position;
        Player.main.SetPosition(positionInVehicle, rotation * vehicleAngle);
        Player.main.cinematicModeActive = false;
        Player.main.UpdateIsUnderwater();
    }

    private void AttachPlayerToEscapePod(NitroxId escapePodId)
    {
        // The NitroxId passed in here will never exist. What to do?
        //GameObject escapePod = NitroxEntity.RequireObjectFrom(escapePodId);
        
        // NitroxLifepodDrop.main.transform.position = escapePod.transform.position;
        // NitroxLifepodDrop.main.RequireComponent<RespawnPoint>().transform.position = escapePod.transform.position + spawnRelativeToEscapePod;

        // Player.main.transform.position = NitroxLifepodDrop.main.RequireComponent<RespawnPoint>().GetSpawnPosition();

        // Player.main.EnterInterior(NitroxLifepodDrop.main.RequireComponent<LifepodDrop>());
    }

}
