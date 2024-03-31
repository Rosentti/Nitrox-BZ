using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic.FMOD;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

public class MedicalCabinetClickedProcessor : ClientPacketProcessor<MedicalCabinetClicked>
{
    public override void Process(MedicalCabinetClicked packet)
    {
        GameObject gameObject = NitroxEntity.RequireObjectFrom(packet.Id);
        MedicalCabinet cabinet = gameObject.RequireComponent<MedicalCabinet>();

        bool medkitPickedUp = !packet.HasMedKit && cabinet.hasMedKit;
        bool doorChangedState = cabinet.doorOpen != packet.DoorOpen;

        cabinet.hasMedKit = packet.HasMedKit;
        cabinet.timeSpawnMedKit = packet.NextSpawnTime;

        using (PacketSuppressor<FMODCustomEmitterPacket>.Suppress())
        using (FMODSystem.SuppressSubnauticaSounds())
        {
            if (doorChangedState)
            {
                cabinet.Invoke(nameof(MedicalCabinet.ToggleDoorState), 0f);
            }
            else if (medkitPickedUp)
            {
                cabinet.Invoke(nameof(MedicalCabinet.ToggleDoorState), 2f);
            }
        }
    }
}
