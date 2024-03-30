using NitroxClient_Subnautica.Communication.Abstract;
using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.GameLogic.Helper;
using NitroxClient_Subnautica.MonoBehaviours;
using NitroxModel.Packets;
using UnityEngine;
using static NitroxClient_Subnautica.GameLogic.Helper.TransientLocalObjectManager;

namespace NitroxClient_Subnautica.Communication.Packets.Processors
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
