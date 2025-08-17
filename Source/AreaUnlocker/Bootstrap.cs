using System.Reflection;
using HarmonyLib;
using Verse;

namespace AreaUnlocker;

public class Bootstrap : Mod
{
    public Bootstrap(ModContentPack content) : base(content)
    {
        new Harmony("fluffy.areaunlocker").PatchAll(Assembly.GetExecutingAssembly());
    }
}