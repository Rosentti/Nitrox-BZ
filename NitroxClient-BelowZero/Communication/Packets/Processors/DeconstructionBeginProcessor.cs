using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic.Helper;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel.Packets;
using UnityEngine;
using static NitroxClient_BelowZero.GameLogic.Helper.TransientLocalObjectManager;

namespace NitroxClient_BelowZero.Communication.Packets.Processors
{
    public class DeconstructionBeginProcessor : ClientPacketProcessor<DeconstructionBegin>
    {
        private readonly IPacketSender packetSender;

        public DeconstructionBeginProcessor(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public override void Process(DeconstructionBegin packet)
        {
            Log.Info($"Received deconstruction packet for id: {packet.Id}");

            GameObject deconstructing = NitroxEntity.RequireObjectFrom(packet.Id);

            Constructable constructable = deconstructing.GetComponent<Constructable>();
            BaseDeconstructable baseDeconstructable = deconstructing.GetComponent<BaseDeconstructable>();

            using (PacketSuppressor<DeconstructionBegin>.Suppress())
            {
                if (baseDeconstructable != null)
                {
                    TransientLocalObjectManager.Add(TransientObjectType.LATEST_DECONSTRUCTED_BASE_PIECE_GUID, packet.Id);
                    baseDeconstructable.Deconstruct();
                }
                else if (constructable != null)
                {
                    constructable.SetState(false, false);
                }
            }
        }
    }
}
