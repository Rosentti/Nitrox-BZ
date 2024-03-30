namespace NitroxClient_Subnautica.Communication.Abstract
{
    public interface IMultiplayerSessionConnectionContext : IMultiplayerSessionState
    {
        void UpdateConnectionState(IMultiplayerSessionConnectionState sessionConnectionState);
        void ClearSessionState();
    }
}
