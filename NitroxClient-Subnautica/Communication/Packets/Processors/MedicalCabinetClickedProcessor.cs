using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.GameLogic.FMOD;
using NitroxClient_Subnautica.MonoBehaviours;
using NitroxClient_Subnautica.Unity.Helper;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient_Subnautica.Communication.Packets.Processors;

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
