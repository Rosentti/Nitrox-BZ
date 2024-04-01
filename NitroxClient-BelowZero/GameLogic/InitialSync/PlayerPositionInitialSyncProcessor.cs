using System;
using System.Collections;
using System.Diagnostics;
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
    private static readonly Vector3 spawnRelativeToLifepod = new Vector3(0f, 1.5f, 0);

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

        AttachPlayerToLifepod(packet.AssignedEscapePodId);

        Vector3 position = packet.PlayerSpawnData.ToUnity();
        Quaternion rotation = packet.PlayerSpawnRotation.ToUnity();
        if (Math.Abs(position.x) < 0.0002 && Math.Abs(position.y) < 0.0002 && Math.Abs(position.z) < 0.0002)
        {
            position = Player.mainObject.transform.position;
        }
        Player.main.SetPosition(position, rotation);

        // Player position is relative to a subroot if in a subroot
        Optional<NitroxId> subRootId = packet.PlayerSubRootId;
        Log.Info($"Initial sync SubRoot: {subRootId}");

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
            Log.Debug("SubRootId-GameObject has no SubRoot component, so it's assumed to be the Lifepod");
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

    private void AttachPlayerToLifepod(NitroxId escapePodId)
    {
        GameObject lifepod = NitroxEntity.RequireObjectFrom(escapePodId);
        Trace.Assert(lifepod != null);

        NitroxLifepodDrop.main = lifepod;
        NitroxLifepodDrop.drop.dropZone = new SupplyDropZone(lifepod.transform.position);
        NitroxLifepodDrop.main.transform.position = lifepod.transform.position;
        NitroxLifepodDrop.respawnPoint.transform.position = lifepod.transform.position;

        // Remove parachute, add legs, adjust ping
        NitroxLifepodDrop.drop.dropComplete = true;
        NitroxLifepodDrop.drop.SetPingInstanceToPod();
		NitroxLifepodDrop.drop.DisableParachute();
		NitroxLifepodDrop.drop.podAnimator.SetTrigger("leg_extension");
		NitroxLifepodDrop.drop.podAnimator.SetTrigger("solar_deploy");
        NitroxLifepodDrop.drop.StartExtendLegs();

        Player.main.transform.position = NitroxLifepodDrop.respawnPoint.GetSpawnPosition() + spawnRelativeToLifepod;
        Player.main.transform.rotation = NitroxLifepodDrop.main.transform.rotation;
    }

}
