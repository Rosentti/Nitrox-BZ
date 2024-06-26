using System.Collections.Generic;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel_BelowZero.DataStructures;
using NitroxServer.GameLogic.Entities;

namespace NitroxServer_BelowZero.GameLogic.Entities;

public class SimulationWhitelist : ISimulationWhitelist
{
    /// <inheritdoc cref="ISimulationWhitelist.MovementWhitelist" />
    public static readonly HashSet<NitroxTechType> MovementWhitelist = new()
    {
        TechType.Shocker.ToDto(),
        TechType.Biter.ToDto(),
        TechType.Blighter.ToDto(),
        TechType.BoneShark.ToDto(),
        TechType.Crabsnake.ToDto(),
        TechType.CrabSquid.ToDto(),
        TechType.Crash.ToDto(),
        TechType.GhostLeviathan.ToDto(),
        TechType.GhostLeviathanJuvenile.ToDto(),
        TechType.GhostRayBlue.ToDto(),
        TechType.GhostRayRed.ToDto(),
        TechType.Mesmer.ToDto(),
        TechType.LavaLizard.ToDto(),
        TechType.LavaEyeye.ToDto(),
        TechType.LavaBoomerang.ToDto(),
        TechType.LargeFloater.ToDto(),
        TechType.LargeKoosh.ToDto(),
        TechType.SpineEel.ToDto(),
        TechType.Spinefish.ToDto(),
        TechType.Sandshark.ToDto(),
        TechType.SeaDragon.ToDto(),
        TechType.SeaEmperor.ToDto(),
        TechType.SeaEmperorBaby.ToDto(),
        TechType.SeaEmperorJuvenile.ToDto(),
        TechType.SeaEmperorLeviathan.ToDto(),
        TechType.ReaperLeviathan.ToDto(),
        TechType.Stalker.ToDto(),
        TechType.Warper.ToDto(),
        TechType.Bladderfish.ToDto(),
        TechType.Boomerang.ToDto(),
        TechType.Cutefish.ToDto(),
        TechType.Eyeye.ToDto(),
        TechType.Jellyray.ToDto(),
        TechType.GarryFish.ToDto(),
        TechType.Gasopod.ToDto(),
        TechType.HoleFish.ToDto(),
        TechType.Hoopfish.ToDto(),
        TechType.Hoverfish.ToDto(),
        TechType.Oculus.ToDto(),
        TechType.RabbitRay.ToDto(),
        TechType.Reefback.ToDto(),
        TechType.Reginald.ToDto(),
        TechType.SeaTreader.ToDto(),
        TechType.Skyray.ToDto(),
        TechType.Spadefish.ToDto(),
        TechType.Spinefish.ToDto(),
        TechType.BlueAmoeba.ToDto(),
        TechType.Shuttlebug.ToDto(),
        TechType.CaveCrawler.ToDto(),
        TechType.Floater.ToDto(),
        TechType.LavaLarva.ToDto(),
        TechType.Rockgrub.ToDto(),
        TechType.Shuttlebug.ToDto(),
        TechType.Bloom.ToDto(),
        TechType.RockPuncher.ToDto(),
        TechType.Peeper.ToDto(),
        TechType.Jumper.ToDto(),
        TechType.Penguin.ToDto(),
        TechType.PenguinBaby.ToDto(),
        TechType.Constructor.ToDto()
    };

    /// <inheritdoc cref="ISimulationWhitelist.UtilityWhitelist" />
    public static readonly HashSet<NitroxTechType> UtilityWhitelist = new()
    {
        TechType.CrashHome.ToDto()
    };

    HashSet<NitroxTechType> ISimulationWhitelist.MovementWhitelist => MovementWhitelist;
    HashSet<NitroxTechType> ISimulationWhitelist.UtilityWhitelist => UtilityWhitelist;
}
