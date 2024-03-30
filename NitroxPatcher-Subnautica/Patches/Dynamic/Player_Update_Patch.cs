using System.Reflection;
using NitroxClient_Subnautica.MonoBehaviours.Gui.Input;
using NitroxClient_Subnautica.MonoBehaviours.Gui.Input.KeyBindings;
using NitroxModel.Helper;

namespace NitroxPatcher_Subnautica.Patches.Dynamic;

public sealed partial class Player_Update_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((Player t) => t.Update());

    public static void Postfix(Player __instance)
    {
        // TODO: Use proper way to check if input is free, because players can be editing labels etc.
        if (DevConsole.instance.state)
        {
            return;
        }

        KeyBindingManager keyBindingManager = new();
        foreach (KeyBinding keyBinding in keyBindingManager.KeyboardKeyBindings)
        {
            if (GameInput.GetButtonDown(keyBinding.Button))
            {
                keyBinding.Action.Execute();
            }
        }
    }
}
