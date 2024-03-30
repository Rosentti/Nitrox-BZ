using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.GameLogic;
using NitroxModel.Packets;

namespace NitroxClient_Subnautica.Communication.Packets.Processors;

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
