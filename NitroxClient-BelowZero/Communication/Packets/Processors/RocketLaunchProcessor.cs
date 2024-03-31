using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic;
using NitroxModel_BelowZero.Packets;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

public class RocketLaunchProcessor : ClientPacketProcessor<RocketLaunch>
{
    private readonly Rockets rockets;

    public RocketLaunchProcessor(Rockets rockets)
    {
        this.rockets = rockets;
    }

    public override void Process(RocketLaunch rocketLaunch)
    {
        rockets.RocketLaunch(rocketLaunch.RocketId);        
    }
}
