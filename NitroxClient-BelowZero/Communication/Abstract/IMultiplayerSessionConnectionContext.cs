namespace NitroxClient_BelowZero.Communication.Abstract
{
    public interface IMultiplayerSessionConnectionContext : IMultiplayerSessionState
    {
        void UpdateConnectionState(IMultiplayerSessionConnectionState sessionConnectionState);
        void ClearSessionState();
    }
}
