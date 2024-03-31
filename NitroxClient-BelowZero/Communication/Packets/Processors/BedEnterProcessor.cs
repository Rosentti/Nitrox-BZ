using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxModel.Packets;

namespace NitroxClient_BelowZero.Communication.Packets.Processors
{
    class BedEnterProcessor : ClientPacketProcessor<BedEnter>
    {
        public override void Process(BedEnter packet)
        {
        }
    }
}
