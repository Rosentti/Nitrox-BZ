using System.Threading.Tasks;
using NitroxClient_BelowZero.Communication.MultiplayerSession;

namespace NitroxClient_BelowZero.Communication.Abstract
{
    public interface IMultiplayerSessionConnectionState
    {
        MultiplayerSessionConnectionStage CurrentStage { get; }
        
        Task NegotiateReservationAsync(IMultiplayerSessionConnectionContext sessionConnectionContext);
        void JoinSession(IMultiplayerSessionConnectionContext sessionConnectionContext);
        void Disconnect(IMultiplayerSessionConnectionContext sessionConnectionContext);
    }
}
