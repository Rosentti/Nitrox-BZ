using System.Reflection;
using NitroxClient_Subnautica.GameLogic;
using NitroxModel.Helper;

namespace NitroxPatcher_Subnautica.Patches.Dynamic;

public sealed partial class ItemsContainer_NotifyAddItem_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((ItemsContainer t) => t.NotifyAddItem(default));

    public static void Postfix(ItemsContainer __instance, InventoryItem item)
    {
        if (item != null)
        {
            Resolve<ItemContainers>().BroadcastItemAdd(item.item, __instance.tr);
        }
    }
}
