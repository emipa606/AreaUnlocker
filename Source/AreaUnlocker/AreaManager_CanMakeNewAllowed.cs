using HarmonyLib;
using Verse;

namespace AreaUnlocker;

[HarmonyPatch(typeof(AreaManager), nameof(AreaManager.CanMakeNewAllowed))]
public static class AreaManager_CanMakeNewAllowed
{
    private static bool Prefix(ref bool __result)
    {
        __result = true;
        return false;
    }
}