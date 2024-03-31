using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel.Packets;
using NitroxModel_BelowZero.DataStructures;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

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
