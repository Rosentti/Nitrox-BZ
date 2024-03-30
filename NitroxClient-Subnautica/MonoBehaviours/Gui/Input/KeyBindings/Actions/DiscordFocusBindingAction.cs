using NitroxClient_Subnautica.MonoBehaviours.Discord;
using UnityEngine;

namespace NitroxClient_Subnautica.MonoBehaviours.Gui.Input.KeyBindings.Actions;

public class DiscordFocusBindingAction : KeyBindingAction
{
    public override void Execute()
    {
        if (UnityEngine.Input.GetKey(KeyCode.LeftAlt))
        {
            DiscordJoinRequestGui.Select();
        }
    }
}
