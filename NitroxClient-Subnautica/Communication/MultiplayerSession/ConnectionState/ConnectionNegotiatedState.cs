using System;
using System.Threading.Tasks;
using NitroxClient_Subnautica.Communication.Abstract;

namespace NitroxClient_Subnautica.Communication.MultiplayerSession.ConnectionState
{
    public abstract class ConnectionNegotiatedState : CommunicatingState
    {
        public override Task NegotiateReservationAsync(IMultiplayerSessionConnectionContext sessionConnectionContext)
        {
            throw new InvalidOperationException("Unable to negotiate a session connection in the current state.");
        }
    }
}
