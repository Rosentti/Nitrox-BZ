using System;
using System.Collections.Generic;

namespace NitroxClient_Subnautica.GameLogic.PlayerLogic.PlayerPreferences
{
    [Serializable]
    public class PlayerPreferenceState
    {
        public PlayerPreference LastSetPlayerPreference;
        public Dictionary<string, PlayerPreference> Preferences;
    }
}
