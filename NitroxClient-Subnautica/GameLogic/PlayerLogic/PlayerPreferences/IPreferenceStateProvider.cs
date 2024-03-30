namespace NitroxClient_Subnautica.GameLogic.PlayerLogic.PlayerPreferences
{
    public interface IPreferenceStateProvider
    {
        PlayerPreferenceState GetPreferenceState();
        void SavePreferenceState(PlayerPreferenceState preferenceState);
    }
}
