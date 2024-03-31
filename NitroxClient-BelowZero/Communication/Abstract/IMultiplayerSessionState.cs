using NitroxModel.MultiplayerSession;
using NitroxModel.Packets;

namespace NitroxClient_BelowZero.Communication.Abstract
{
    public interface IMultiplayerSessionState
    {
        IClient Client { get; }
        string IpAddress { get; }
        int ServerPort { get; }
        MultiplayerSessionPolicy SessionPolicy { get; }
        PlayerSettings PlayerSettings { get; }
        AuthenticationContext AuthenticationContext { get; }
        MultiplayerSessionReservation Reservation { get; }
    }
}
