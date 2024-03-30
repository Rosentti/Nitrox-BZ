using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.GameLogic;
using NitroxModel_Subnautica.Packets;

namespace NitroxClient_Subnautica.Communication.Packets.Processors;

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
