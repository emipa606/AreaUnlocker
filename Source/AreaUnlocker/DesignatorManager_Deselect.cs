using HarmonyLib;
using RimWorld;
using Verse;

namespace AreaUnlocker;

[HarmonyPatch(typeof(DesignatorManager), nameof(DesignatorManager.Deselect))]
public static class DesignatorManager_Deselect
{
    public static void Postfix()
    {
        if (!Dialog_ManageAreas_DoWindowContents.ShouldReopen ||
            Dialog_ManageAreas_DoWindowContents.LastManagedMap == null)
        {
            return;
        }

        Dialog_ManageAreas_DoWindowContents.ShouldReopen = false;
        Find.WindowStack.Add(new Dialog_ManageAreas(Dialog_ManageAreas_DoWindowContents.LastManagedMap));
    }
}