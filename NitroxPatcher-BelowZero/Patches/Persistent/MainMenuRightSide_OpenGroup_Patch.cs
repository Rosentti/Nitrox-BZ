﻿using System.Reflection;
using NitroxClient_BelowZero.MonoBehaviours.Gui.MainMenu;
using NitroxModel.Helper;

namespace NitroxPatcher_BelowZero.Patches.Persistent
{
    public partial class MainMenuRightSide_OpenGroup_Patch : NitroxPatch, IPersistentPatch
    {
        private static readonly MethodInfo TARGET_METHOD = Reflect.Method((MainMenuRightSide t) => t.OpenGroup(default(string)));

        public static void Prefix(string target)
        {
            // Don't stop the client if the client is trying to connect (in the case: target = "Join Server")
            // We can detect that the Join Server tab is still available because the gameobject is activated only when the tab is opened
            if (MainMenuMultiplayerPanel.Main.JoinServer.gameObject.activeSelf && !target.Equals(MainMenuMultiplayerPanel.Main.JoinServer.MenuName))
            {
                MainMenuMultiplayerPanel.Main.JoinServer.StopMultiplayerClient();
            }
        }
    }
}
