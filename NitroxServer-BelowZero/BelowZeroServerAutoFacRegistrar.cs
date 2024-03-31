using System.Collections.Generic;
using Autofac;
using NitroxModel;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.DataStructures.GameLogic.Entities;
using NitroxModel.GameLogic.FMOD;
using NitroxModel.Helper;
using NitroxModel_BelowZero.DataStructures;
using NitroxModel_BelowZero.DataStructures.GameLogic.Entities;
using NitroxModel_BelowZero.Helper;
using NitroxServer;
using NitroxServer.GameLogic;
using NitroxServer.GameLogic.Entities;
using NitroxServer.GameLogic.Entities.Spawning;
using NitroxServer.Serialization;
using NitroxServer_BelowZero.GameLogic;
using NitroxServer_BelowZero.GameLogic.Entities;
using NitroxServer_BelowZero.GameLogic.Entities.Spawning;
using NitroxServer_BelowZero.GameLogic.Entities.Spawning.EntityBootstrappers;
using NitroxServer_BelowZero.Resources;
using NitroxServer_BelowZero.Serialization;

namespace NitroxServer_BelowZero
{
    public class BelowZeroServerAutoFacRegistrar : ServerAutoFacRegistrar
    {
        public override void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            containerBuilder.Register(c => GameInfo.SubnauticaBelowZero);
            base.RegisterDependencies(containerBuilder);

            containerBuilder.RegisterType<SimulationWhitelist>()
                            .As<ISimulationWhitelist>()
                            .SingleInstance();
            containerBuilder.Register(c => new BelowZeroServerProtoBufSerializer(
                                          "Assembly-CSharp",
                                          "Assembly-CSharp-firstpass",
                                          "NitroxModel",
                                          "NitroxModel-BelowZero"))
                            .As<ServerProtoBufSerializer, IServerSerializer>()
                            .SingleInstance();
            containerBuilder.Register(c => new BelowZeroServerJsonSerializer())
                            .As<ServerJsonSerializer, IServerSerializer>()
                            .SingleInstance();

            containerBuilder.RegisterType<BelowZeroEntitySpawnPointFactory>().As<EntitySpawnPointFactory>().SingleInstance();

            ResourceAssets resourceAssets = ResourceAssetsParser.Parse();

            containerBuilder.Register(c => resourceAssets).SingleInstance();
            containerBuilder.Register(c => resourceAssets.WorldEntitiesByClassId).SingleInstance();
            containerBuilder.Register(c => resourceAssets.PrefabPlaceholdersGroupsByGroupClassId).SingleInstance();
            containerBuilder.Register(c => resourceAssets.NitroxRandom).SingleInstance();
            containerBuilder.RegisterType<SubnauticaUweWorldEntityFactory>().As<IUweWorldEntityFactory>().SingleInstance();

            SubnauticaUwePrefabFactory prefabFactory = new SubnauticaUwePrefabFactory(resourceAssets.LootDistributionsJson);
            containerBuilder.Register(c => prefabFactory).As<IUwePrefabFactory>().SingleInstance();
            containerBuilder.RegisterType<SubnauticaEntityBootstrapperManager>()
                            .As<IEntityBootstrapperManager>()
                            .SingleInstance();

            containerBuilder.RegisterType<BelowZeroMap>().As<IMap>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<EntityRegistry>().AsSelf().InstancePerLifetimeScope();
            containerBuilder.RegisterType<SubnauticaWorldModifier>().As<IWorldModifier>().InstancePerLifetimeScope();
            containerBuilder.Register(c => new FMODWhitelist(GameInfo.SubnauticaBelowZero)).InstancePerLifetimeScope();
        }
    }
}
