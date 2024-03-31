namespace NitroxClient_BelowZero.GameLogic.PlayerLogic.PlayerPreferences
{
    public interface IPreferenceStateProvider
    {
        PlayerPreferenceState GetPreferenceState();
        void SavePreferenceState(PlayerPreferenceState preferenceState);
    }
}
