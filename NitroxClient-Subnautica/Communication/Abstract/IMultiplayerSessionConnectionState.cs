using System.Threading.Tasks;
using NitroxClient_Subnautica.Communication.MultiplayerSession;

namespace NitroxClient_Subnautica.Communication.Abstract
{
    public interface IMultiplayerSessionConnectionState
    {
        MultiplayerSessionConnectionStage CurrentStage { get; }
        
        Task NegotiateReservationAsync(IMultiplayerSessionConnectionContext sessionConnectionContext);
        void JoinSession(IMultiplayerSessionConnectionContext sessionConnectionContext);
        void Disconnect(IMultiplayerSessionConnectionContext sessionConnectionContext);
    }
}
