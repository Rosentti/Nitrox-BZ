using NitroxClient_BelowZero.Communication.Abstract;
using NitroxModel.DataStructures;
using NitroxModel_BelowZero.DataStructures.GameLogic.Creatures.Actions;
using NitroxModel_BelowZero.Packets;

namespace NitroxClient_BelowZero.GameLogic
{
    public class AI
    {
        private readonly IPacketSender packetSender;

        public AI(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public void CreatureActionChanged(NitroxId id, CreatureAction newAction)
        {
            SerializableCreatureAction creatureAction = null;

            /*
            Example for next implementation:

            if (newAction.GetType() == typeof(SwimToPoint))
            {
                creatureAction = new SwimToPointAction(((SwimToPoint)newAction).Target);
            }*/

            if (creatureAction != null)
            {
                CreatureActionChanged actionChanged = new CreatureActionChanged(id, creatureAction);
                packetSender.Send(actionChanged);
            }
        }
    }
}
