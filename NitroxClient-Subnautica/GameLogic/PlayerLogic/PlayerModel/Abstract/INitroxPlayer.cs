using NitroxModel.MultiplayerSession;
using UnityEngine;

namespace NitroxClient_Subnautica.GameLogic.PlayerLogic.PlayerModel.Abstract
{
    public interface INitroxPlayer
    {
        GameObject Body { get; }
        GameObject PlayerModel { get; }
        string PlayerName { get; }
        PlayerSettings PlayerSettings { get; }
    }
}
