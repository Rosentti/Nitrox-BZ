using System.Collections.Generic;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.Helper;

namespace NitroxModel_BelowZero.Helper
{
    /// <summary>
    ///     Static information about the game world loaded by Below Zero that isn't (and shouldn't) be retrievable from the game directly. 
    /// </summary>
    public class BelowZeroMap : IMap
    {
        private const int BATCH_SIZE = 160;
        private const int SKYBOX_METERS_ABOVE_WATER = 160;

        /// <summary>
        ///     TechType can't be introspected at runtime in RELEASE mode because its reflection info is elided.
        /// </summary>
        public static readonly List<NitroxTechType> GLOBAL_ROOT_TECH_TYPES = new List<NitroxTechType>
        {

            // I found the below bit in CompileTimeCheck of LargeWorldEntity. Is this correct? These types don't seem to match directly and some don't seem to have tech types
            //     if ((bool)GetComponent<Base>() || (bool)GetComponent<LifepodDrop>() || (bool)GetComponent<ConstructableBase>() || (bool)GetComponent<MapRoomCamera>() || (bool)GetComponent<Exosuit>() || (bool)GetComponent<Rocket>() || (bool)GetComponent<SeaMoth>() || (bool)GetComponent<SeaTruckSegment>() || (bool)GetComponent<SupplyDrop>() || (bool)GetComponent<SpyPenguin>() || (bool)GetComponentInChildren<IceWormPhantomManager>())
            // {
            // 	return null;
            // }
            // if ((bool)GetComponent<Accumulator>() || (bool)GetComponent<SolarPanel>() || (bool)GetComponent<ThermalPlant>() || (bool)GetComponent<PowerRelay>() || (bool)GetComponent<Planter>() || (bool)GetComponent<QuantumLockerStorage>())
            // {
            // 	return null;
            // }
            // if ((bool)GetComponent<Beacon>() || (bool)GetComponent<DiveReel>() || (bool)GetComponent<DiveReelAnchor>() || (bool)GetComponent<Constructor>() || (bool)GetComponent<Hoverbike>() || (bool)GetComponent<Thumper>() || (bool)GetComponent<PipeSurfaceFloater>() || (bool)GetComponent<BasePipeConnector>() || (bool)GetComponent<Pipe>() || (bool)GetComponent<PlayerSoundTrigger>() || (bool)GetComponent<RegeneratePowerSource>() || (bool)GetComponent<SignalPing>() || (bool)GetComponent<DeployableStorage>() || (bool)GetComponent<LEDLight>())
            // {
            // 	return null;
            // }

            new NitroxTechType(nameof(TechType.Pipe)),
            new NitroxTechType(nameof(TechType.MapRoomCamera)),
            new NitroxTechType(nameof(TechType.Exosuit)),
            new NitroxTechType(nameof(TechType.RocketBase)),
            new NitroxTechType(nameof(TechType.Seamoth)),
            new NitroxTechType(nameof(TechType.SupplyDrop)),
            new NitroxTechType(nameof(TechType.SpyPenguin)),
            new NitroxTechType(nameof(TechType.Accumulator)),
            new NitroxTechType(nameof(TechType.SolarPanel)),
            new NitroxTechType(nameof(TechType.ThermalPlant)),
            new NitroxTechType(nameof(TechType.PowerTransmitter)),
            new NitroxTechType(nameof(TechType.BasePlanter)),
            new NitroxTechType(nameof(TechType.QuantumLocker)),
            new NitroxTechType(nameof(TechType.DiveReel)),
            new NitroxTechType(nameof(TechType.Hoverbike)),
            new NitroxTechType(nameof(TechType.Thumper)),
            new NitroxTechType(nameof(TechType.Signal)),
            new NitroxTechType(nameof(TechType.Constructor)),
            new NitroxTechType(nameof(TechType.Flare)),
            new NitroxTechType(nameof(TechType.Gravsphere)),
            new NitroxTechType(nameof(TechType.PipeSurfaceFloater)),
            new NitroxTechType(nameof(TechType.SmallStorage)),
            new NitroxTechType(nameof(TechType.CyclopsDecoy)),
            new NitroxTechType(nameof(TechType.LEDLight)),
            new NitroxTechType(nameof(TechType.Beacon))
        };

        public int ItemLevelOfDetail => 3;
        public int BatchSize => 160;
        public NitroxInt3 BatchDimensions => new NitroxInt3(BatchSize, BatchSize, BatchSize);
        public NitroxInt3 DimensionsInMeters => new NitroxInt3(4096, 3200, 4096);
        public NitroxInt3 DimensionsInBatches => NitroxInt3.Ceil(DimensionsInMeters / BATCH_SIZE);
        public NitroxInt3 BatchDimensionCenter => new NitroxInt3(DimensionsInMeters.X / 2, DimensionsInMeters.Y - SKYBOX_METERS_ABOVE_WATER, DimensionsInMeters.Z / 2);
        public List<NitroxTechType> GlobalRootTechTypes { get; } = GLOBAL_ROOT_TECH_TYPES;
    }
}
