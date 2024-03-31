using System.Collections.Generic;
using System.Linq;
using NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor.Abstract;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;

namespace NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor;

public class RocketMetadataExtractor : EntityMetadataExtractor<Rocket, RocketMetadata>
{
    public override RocketMetadata Extract(Rocket rocket)
    {
        RocketPreflightCheckManager rocketPreflightCheckManager = rocket.RequireComponent<RocketPreflightCheckManager>();
        List<int> prechecks = rocketPreflightCheckManager.preflightChecks.Select(i => (int)i).ToList();

        return new(rocket.currentRocketStage, DayNightCycle.main.timePassedAsFloat, (int)rocket.elevatorState, rocket.elevatorPosition, prechecks);
    }
}
