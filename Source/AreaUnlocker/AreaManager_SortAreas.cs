using HarmonyLib;
using Verse;

namespace AreaUnlocker;

[HarmonyPatch(typeof(AreaManager), "SortAreas")]
public static class AreaManager_SortAreas
{
    public static bool Prefix()
    {
        return false;
    }
}