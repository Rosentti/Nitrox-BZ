using System;
using NitroxClient_BelowZero.Communication.Abstract;

namespace NitroxClient_BelowZero.Communication.MultiplayerSession.ConnectionState
{
    public abstract class ConnectionNegotiatingState : CommunicatingState
    {
        public override void JoinSession(IMultiplayerSessionConnectionContext sessionConnectionContext)
        {
            throw new InvalidOperationException("Cannot join a session until a reservation has been negotiated with the server.");
        }
    }
}
