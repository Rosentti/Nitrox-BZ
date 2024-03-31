using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxModel.Packets;
using Story;

namespace NitroxClient_BelowZero.Communication.Packets.Processors
{
    public class RadioPlayPendingMessageProcessor : ClientPacketProcessor<RadioPlayPendingMessage>
    {
        public override void Process(RadioPlayPendingMessage packet)
        {
            StoryGoalManager.main.ExecutePendingRadioMessage();
        }
    }
}
