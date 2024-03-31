using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic;
using NitroxModel.Packets;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

public class TimeChangeProcessor : ClientPacketProcessor<TimeChange>
{
    private readonly TimeManager timeManager;

    public TimeChangeProcessor(TimeManager timeManager)
    {
        this.timeManager = timeManager;
    }

    public override void Process(TimeChange timeChangePacket)
    {
        timeManager.ProcessUpdate(timeChangePacket);
    }
}
