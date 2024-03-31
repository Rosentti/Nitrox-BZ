using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic;
using NitroxModel.Packets;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

public class AuroraAndTimeUpdateProcessor : ClientPacketProcessor<AuroraAndTimeUpdate>
{
    private readonly TimeManager timeManager;

    public AuroraAndTimeUpdateProcessor(TimeManager timeManager)
    {
        this.timeManager = timeManager;
    }

    public override void Process(AuroraAndTimeUpdate packet)
    {
        timeManager.ProcessUpdate(packet.TimeData.TimePacket);
    }
}
