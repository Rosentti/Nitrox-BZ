using System;
using NitroxClient_Subnautica.Communication.Abstract;

namespace NitroxClient_Subnautica.Communication.MultiplayerSession.ConnectionState
{
    public class SessionReservationRejected : ConnectionNegotiatedState
    {
        public override MultiplayerSessionConnectionStage CurrentStage => MultiplayerSessionConnectionStage.SESSION_RESERVATION_REJECTED;

        public override void JoinSession(IMultiplayerSessionConnectionContext sessionConnectionContext)
        {
            throw new InvalidOperationException("The session has rejected the reserveration request.");
        }
    }
}
