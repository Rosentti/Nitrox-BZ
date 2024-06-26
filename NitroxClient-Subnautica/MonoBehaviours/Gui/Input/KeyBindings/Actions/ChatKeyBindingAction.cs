﻿using NitroxClient_Subnautica.GameLogic.ChatUI;
using NitroxModel.Core;

namespace NitroxClient_Subnautica.MonoBehaviours.Gui.Input.KeyBindings.Actions
{
    public class ChatKeyBindingAction : KeyBindingAction
    {
        public override void Execute()
        {
            // If no other UWE input field is currently active then allow chat to open.
            if (FPSInputModule.current.lastGroup == null)
            {
                NitroxServiceLocator.LocateService<PlayerChatManager>().SelectChat();
            }
        }
    }
}
