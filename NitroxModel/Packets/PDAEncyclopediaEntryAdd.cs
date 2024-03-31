using System;

namespace NitroxModel.Packets;

[Serializable]
public class PDAEncyclopediaEntryAdd : Packet
{
    public string Key { get; }
    /// <summary>
    ///     If true, shows a notification to the player.
    /// </summary>
    public bool Verbose { get; }

    /// <summary>
    ///     (BZ ONLY) Should a notification be shown to the player
    /// </summary>
    public bool PostNotification { get; }

    public PDAEncyclopediaEntryAdd(string key, bool verbose, bool postNotification)
    {
        Key = key;
        Verbose = verbose;
        PostNotification = postNotification;
    }
}
