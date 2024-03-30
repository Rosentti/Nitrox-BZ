using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.MonoBehaviours;
using NitroxModel.Packets;
using NitroxModel_Subnautica.DataStructures;

namespace NitroxClient_Subnautica.Communication.Packets.Processors;

public class LeakRepairedProcessor : ClientPacketProcessor<LeakRepaired>
{
    public override void Process(LeakRepaired packet)
    {
        if (NitroxEntity.TryGetComponentFrom(packet.BaseId, out BaseLeakManager baseLeakManager))
        {
            baseLeakManager.HealLeakToMax(packet.RelativeCell.ToUnity());
        }
    }
}
