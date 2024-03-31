global using NitroxClient_BelowZero.Helpers;
global using NitroxModel.Logger;
global using static NitroxModel.Extensions;
using System.Reflection;
using Autofac;
using Autofac.Core;
using NitroxClient_BelowZero.Communication;
using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.Communication.MultiplayerSession;
using NitroxClient_BelowZero.Communication.NetworkingLayer.LiteNetLib;
using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.Debuggers;
using NitroxClient_BelowZero.Debuggers.Drawer;
using NitroxClient_BelowZero.GameLogic;
using NitroxClient_BelowZero.GameLogic.ChatUI;
using NitroxClient_BelowZero.GameLogic.FMOD;
using NitroxClient_BelowZero.GameLogic.HUD;
using NitroxClient_BelowZero.GameLogic.InitialSync.Abstract;
using NitroxClient_BelowZero.GameLogic.PlayerLogic;
using NitroxClient_BelowZero.GameLogic.PlayerLogic.PlayerModel;
using NitroxClient_BelowZero.GameLogic.PlayerLogic.PlayerModel.Abstract;
using NitroxClient_BelowZero.GameLogic.PlayerLogic.PlayerPreferences;
using NitroxClient_BelowZero.GameLogic.Settings;
using NitroxClient_BelowZero.GameLogic.Spawning.Metadata;
using NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor.Abstract;
using NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Processor.Abstract;
using NitroxClient_BelowZero.Map;
using NitroxModel;
using NitroxModel.Core;
using NitroxModel.GameLogic.FMOD;
using NitroxModel.Helper;
using NitroxModel_BelowZero.Helper;

namespace NitroxClient_BelowZero
{
    public class ClientAutoFacRegistrar : IAutoFacRegistrar
    {
        private static readonly Assembly currentAssembly = Assembly.GetExecutingAssembly();
        private readonly IModule[] modules;

        public ClientAutoFacRegistrar(params IModule[] modules)
        {
            this.modules = modules;
        }

        public void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            foreach (IModule module in modules)
            {
                containerBuilder.RegisterModule(module);
            }

            RegisterCoreDependencies(containerBuilder);
            RegisterMetadataDependencies(containerBuilder);
            RegisterPacketProcessors(containerBuilder);
            RegisterColorSwapManagers(containerBuilder);
            RegisterInitialSyncProcessors(containerBuilder);
        }

        private void RegisterCoreDependencies(ContainerBuilder containerBuilder)
        {
#if DEBUG
            containerBuilder.RegisterAssemblyTypes(currentAssembly)
                            .AssignableTo<BaseDebugger>()
                            .As<BaseDebugger>()
                            .AsImplementedInterfaces()
                            .AsSelf()
                            .SingleInstance();

            containerBuilder.RegisterAssemblyTypes(currentAssembly)
                            .AssignableTo<IDrawer>()
                            .As<IDrawer>()
                            .SingleInstance();

            containerBuilder.RegisterAssemblyTypes(currentAssembly)
                            .AssignableTo<IStructDrawer>()
                            .As<IStructDrawer>()
                            .SingleInstance();
#endif
            containerBuilder.Register(c => new NitroxProtobufSerializer($"{nameof(NitroxModel)}.dll"));

            containerBuilder.RegisterType<UnityPreferenceStateProvider>()
                            .As<IPreferenceStateProvider>()
                            .SingleInstance();

            containerBuilder.RegisterType<PlayerPreferenceManager>().SingleInstance();

            containerBuilder.RegisterType<MultiplayerSessionManager>()
                            .As<IMultiplayerSession>()
                            .As<IPacketSender>()
                            .InstancePerLifetimeScope();

            containerBuilder.RegisterType<LiteNetLibClient>()
                            .As<IClient>()
                            .InstancePerLifetimeScope();

            containerBuilder.RegisterType<LocalPlayer>()
                            .AsSelf() //Would like to deprecate this registration at some point and just work through an abstraction.
                            .As<ILocalNitroxPlayer>()
                            .InstancePerLifetimeScope();

            containerBuilder.RegisterType<BelowZeroMap>()
                            .As<IMap>()
                            .InstancePerLifetimeScope();

            containerBuilder.RegisterType<PlayerManager>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PlayerModelManager>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PlayerVitalsManager>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<VisibleBatches>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<VisibleCells>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PacketReceiver>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<Vehicles>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<AI>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PlayerChatManager>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<SimulationOwnership>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<LiveMixinManager>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<Entities>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<MedkitFabricator>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<Items>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<EquipmentSlots>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ItemContainers>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<Cyclops>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<Rockets>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<MobileVehicleBay>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<Interior>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<NitroxConsole>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<Terrain>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ExosuitModuleEvent>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<SeamothModulesEvent>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<Fires>().InstancePerLifetimeScope();
            containerBuilder.Register(_ => new FMODWhitelist(GameInfo.Subnautica)).InstancePerLifetimeScope();
            containerBuilder.RegisterType<FMODSystem>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<NitroxSettingsManager>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ThrottledPacketSender>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PlayerCinematics>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<NitroxPDATabManager>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TimeManager>().InstancePerLifetimeScope();
        }

        private void RegisterMetadataDependencies(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterAssemblyTypes(currentAssembly)
                            .AssignableTo<IEntityMetadataExtractor>()
                            .As<IEntityMetadataExtractor>()
                            .AsSelf()
                            .SingleInstance();
            containerBuilder.RegisterAssemblyTypes(currentAssembly)
                            .AssignableTo<IEntityMetadataProcessor>()
                            .As<IEntityMetadataProcessor>()
                            .AsSelf()
                            .SingleInstance();
            containerBuilder.RegisterType<EntityMetadataManager>().InstancePerLifetimeScope();
        }

        private void RegisterPacketProcessors(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterAssemblyTypes(currentAssembly)
                .AsClosedTypesOf(typeof(ClientPacketProcessor<>))
                .InstancePerLifetimeScope();
        }

        private void RegisterColorSwapManagers(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterAssemblyTypes(currentAssembly)
                .AssignableTo<IColorSwapManager>()
                .As<IColorSwapManager>()
                .SingleInstance();
        }

        private void RegisterInitialSyncProcessors(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterAssemblyTypes(currentAssembly)
                .AssignableTo<IInitialSyncProcessor>()
                .As<IInitialSyncProcessor>()
                .InstancePerLifetimeScope();
        }
    }
}
