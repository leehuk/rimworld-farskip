using HarmonyLib;
using System.Reflection;
using Verse;


namespace FarskipImmobile
{
    [StaticConstructorOnStartup]
    public static class FarskipImmobile
    {
        static FarskipImmobile()
        {
            var harmony = new Harmony("leeh.farskipimmobile");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
