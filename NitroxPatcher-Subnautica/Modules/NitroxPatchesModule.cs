using System.Reflection;
using Autofac;
using NitroxPatcher_Subnautica.Patches;

namespace NitroxPatcher_Subnautica.Modules;

/// <summary>
///     Simple Dependency Injection (DI) container for registering the patch classes with AutoFac.
/// </summary>
public class NitroxPatchesModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .AssignableTo<IPersistentPatch>()
            .AsImplementedInterfaces().SingleInstance();

        builder
            .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .AssignableTo<IDynamicPatch>()
            .AsImplementedInterfaces().SingleInstance();
    }
}
