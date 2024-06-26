using System;
using System.Reflection;
using HarmonyLib;
using NitroxClient_Subnautica.Communication.Abstract;
using NitroxModel.Helper;
using NitroxModel.Packets;

namespace NitroxPatcher_Subnautica.Patches.Dynamic;

/// We just want to disable all these commands on client-side and redirect them as ConsoleCommand
/// TODO: Remove this file when we'll have the command system
public sealed class StoryGoalCustomEventHandler_OnConsoleCommand_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD_START_STORY = Reflect.Method((StoryGoalCustomEventHandler t) => t.OnConsoleCommand_startsunbeamstoryevent());
    private static readonly MethodInfo TARGET_METHOD_GUN_AIM = Reflect.Method((StoryGoalCustomEventHandler t) => t.OnConsoleCommand_precursorgunaim());
    private static readonly MethodInfo TARGET_METHOD_COUNTDOWN = Reflect.Method((StoryGoalCustomEventHandler t) => t.OnConsoleCommand_sunbeamcountdownstart());

    public static bool PrefixStartStory()
    {
        Resolve<IPacketSender>().Send(new ServerCommand("sunbeam storystart"));
        return false;
    }

    public static bool PrefixGunAim()
    {
        Resolve<IPacketSender>().Send(new ServerCommand("sunbeam gunaim"));
        return false;
    }

    public static bool PrefixCountdown()
    {
        Resolve<IPacketSender>().Send(new ServerCommand("sunbeam countdown"));
        return false;
    }

    public override void Patch(Harmony harmony)
    {
        PatchPrefix(harmony, TARGET_METHOD_START_STORY, ((Func<bool>)PrefixStartStory).Method);
        PatchPrefix(harmony, TARGET_METHOD_COUNTDOWN, ((Func<bool>)PrefixCountdown).Method);
        PatchPrefix(harmony, TARGET_METHOD_GUN_AIM, ((Func<bool>)PrefixGunAim).Method);
    }
}
