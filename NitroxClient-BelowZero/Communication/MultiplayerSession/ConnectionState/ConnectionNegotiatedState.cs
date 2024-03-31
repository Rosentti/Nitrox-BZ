using System;
using System.Threading.Tasks;
using NitroxClient_BelowZero.Communication.Abstract;

namespace NitroxClient_BelowZero.Communication.MultiplayerSession.ConnectionState
{
    public abstract class ConnectionNegotiatedState : CommunicatingState
    {
        public override Task NegotiateReservationAsync(IMultiplayerSessionConnectionContext sessionConnectionContext)
        {
            throw new InvalidOperationException("Unable to negotiate a session connection in the current state.");
        }
    }
}
