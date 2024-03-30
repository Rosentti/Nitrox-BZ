using NitroxClient_Subnautica.Communication.Abstract;
using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.MonoBehaviours.Gui.Modals;
using NitroxModel.Packets;

namespace NitroxClient_Subnautica.Communication.Packets.Processors;

public class ServerStoppedProcessor : ClientPacketProcessor<ServerStopped>
{
    private readonly IClient client;

    public ServerStoppedProcessor(IClient client)
    {
        this.client = client;
    }

    public override void Process(ServerStopped packet)
    {
        // We can send the stop instruction right now instead of waiting for the timeout
        client.Stop();
        Modal.Get<ServerStoppedModal>()?.Show();
    }
}
