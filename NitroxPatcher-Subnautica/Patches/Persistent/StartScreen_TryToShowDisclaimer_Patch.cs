#if DEBUG
using System.Reflection;
using NitroxModel.Helper;

namespace NitroxPatcher_Subnautica.Patches.Persistent;

internal sealed partial class StartScreen_TryToShowDisclaimer_Patch : NitroxPatch, IPersistentPatch
{
    protected override bool CanLoadInBelowZero() => true;
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((StartScreen t) => t.TryToShowDisclaimer());

    /// <summary>
    ///     Speed up startup in development by skipping disclaimer screen.
    /// </summary>
    public static bool Prefix() => false;
}
#endif
