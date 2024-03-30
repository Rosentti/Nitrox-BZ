using Autofac;
using NitroxClient_Subnautica.Debuggers;
using NitroxModel.Core;
using NSubstitute;

namespace Nitrox.Test
{
    public class TestAutoFacRegistrar : IAutoFacRegistrar
    {
        public void RegisterDependencies(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterInstance(Substitute.For<INetworkDebugger>()).As<INetworkDebugger>().SingleInstance();
        }
    }
}
