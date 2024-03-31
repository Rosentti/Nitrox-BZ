using System;
using NitroxClient_BelowZero.Communication.Abstract;

namespace NitroxClient_BelowZero.Communication.MultiplayerSession.ConnectionState
{
    public class SessionJoined : ConnectionNegotiatedState
    {
        public override MultiplayerSessionConnectionStage CurrentStage => MultiplayerSessionConnectionStage.SESSION_JOINED;

        public override void JoinSession(IMultiplayerSessionConnectionContext sessionConnectionContext)
        {
            throw new InvalidOperationException("The session is already in progress.");
        }
    }
}
