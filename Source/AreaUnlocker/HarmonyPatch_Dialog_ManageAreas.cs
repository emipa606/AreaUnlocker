using System;
using System.Linq;
using ColourPicker;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace AreaUnlocker;

[HarmonyPatch(typeof(Dialog_ManageAreas), nameof(Dialog_ManageAreas.DoWindowContents))]
public class HarmonyPatch_Dialog_ManageAreas
{
    private static float height = 100f;

    private static Vector2 scrollposition = Vector2.zero;

    private static int reorderableWidgetId = -1;

    [Obsolete]
    public static bool Prefix(Rect inRect, Map ___map)
    {
        var outRect = new Rect(inRect.xMin, inRect.yMin, inRect.width, inRect.height - 100f);
        var rect = new Rect(inRect.xMin, inRect.yMin, inRect.width - 16f, height);
        var rect2 = new Rect(inRect.xMin, inRect.yMax - 80f, inRect.width, 30f);
        var list = (from a in ___map.areaManager.AllAreas.ToList()
            where visible(a)
            select a).ToList();
        var listing_Standard = new Listing_Standard
        {
            ColumnWidth = rect.width
        };
        if (Event.current.type == EventType.Repaint)
        {
            reorderableWidgetId = ReorderableWidget.NewGroup(delegate(int from, int to)
            {
                from = index(from, ___map);
                to = index(to, ___map);
                ___map.areaManager.AllAreas.Insert(to, ___map.areaManager.AllAreas[from]);
                ___map.areaManager.AllAreas.RemoveAt(from >= to ? from + 1 : from);
            }, ReorderableDirection.Vertical, rect);
        }

        Widgets.BeginScrollView(outRect, ref scrollposition, rect);
        listing_Standard.Begin(rect);
        foreach (var item in list)
        {
            doAreaRow(listing_Standard.GetRect(24f), item, reorderableWidgetId);
            listing_Standard.Gap(6f);
        }

        height = list.Count * 30f;
        listing_Standard.End();
        Widgets.EndScrollView();
        if (Widgets.ButtonText(rect2, "NewArea".Translate()))
        {
            ___map.areaManager.TryMakeNewAllowed(out _);
        }

        return false;
    }

    private static void changeColour(Area_Allowed area)
    {
        Find.WindowStack.Add(new Dialog_ColourPicker(area.Color, delegate(Color colour)
        {
            Traverse.Create(area).Field("colorInt").SetValue(colour);
            Traverse.Create(area).Field("colorTextureInt").SetValue(null);
            Traverse.Create(area).Field("drawer").SetValue(null);
        }));
    }

    private static bool visible(Area area)
    {
        if (!area.Mutable)
        {
            return area is Area_Home;
        }

        return true;
    }

    private static int index(int raw, Map map)
    {
        var allAreas = map.areaManager.AllAreas;
        var num = 0;
        var num2 = 0;
        while (num2 < raw && num < allAreas.Count)
        {
            if (visible(allAreas[num++]))
            {
                num2++;
            }
        }

        return num;
    }

    private static void copy(Area area)
    {
        var taggedString = "Fluffy.AreaUnlocker.CopiedAreaLabel".Translate(area.Label);
        var num = 1;
        while (area.areaManager.GetLabeled(taggedString) != null)
        {
            taggedString = "Fluffy.AreaUnlocker.CopiedAreaDuplicateLabel".Translate(area.Label, num++);
        }

        area.areaManager.TryMakeNewAllowed(out var area2);
        Traverse.Create(area2).Field("labelInt").SetValue(taggedString.Resolve());
        foreach (var activeCell in area.ActiveCells)
        {
            area2[activeCell] = true;
        }
    }

    private static void doAreaRow(Rect rect, Area area, int reorderableWidgetId)
    {
        if (Mouse.IsOver(rect))
        {
            area.MarkForDraw();
            GUI.color = area.Color;
            Widgets.DrawHighlight(rect);
            GUI.color = Color.white;
        }

        GUI.DrawTexture(new Rect(rect.xMin, rect.yMin, 16f, 16f).CenteredOnYIn(rect), Icons.DragHash);
        ReorderableWidget.Reorderable(reorderableWidgetId, rect);
        rect.xMin += 20f;
        GUI.BeginGroup(rect);
        var widgetRow = new WidgetRow(0f, 0f);
        var areaAllowed = area as Area_Allowed;
        var foundArea = areaAllowed != null;
        if (foundArea)
        {
            if (widgetRow.ButtonIcon(areaAllowed.ColorTexture, "Fluffy.AreaUnlocker.ChangeColour".Translate(),
                    GenUI.SubtleMouseoverColor))
            {
                changeColour(areaAllowed);
            }
        }
        else
        {
            widgetRow.Icon(area.ColorTexture);
        }

        widgetRow.Gap(4f);
        widgetRow.Label(area.Label, rect.width - ((foundArea ? 6 : 3) * 28f));
        if (foundArea && widgetRow.ButtonIcon(Icons.Rename, "Rename".Translate(), GenUI.MouseoverColor))
        {
            Find.WindowStack.Add(new Dialog_RenameArea(area));
        }

        if (foundArea && widgetRow.ButtonIcon(Icons.Palette, "Fluffy.AreaUnlocker.ChangeColour".Translate(),
                GenUI.MouseoverColor))
        {
            changeColour(areaAllowed);
        }

        if (widgetRow.ButtonIcon(Icons.Invert, "InvertArea".Translate(), GenUI.MouseoverColor))
        {
            area.Invert();
        }

        if (widgetRow.ButtonIcon(Icons.Copy, "Fluffy.AreaUnlocker.CopyArea".Translate(), GenUI.MouseoverColor))
        {
            copy(area);
        }

        if (foundArea && widgetRow.ButtonIcon(Icons.Delete, "Delete".Translate(), GenUI.MouseoverColor))
        {
            area.Delete();
        }

        GUI.EndGroup();
    }
}