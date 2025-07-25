using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Verse;

namespace FarskipImmobile
{
    [HarmonyPatch()]
    public static class UniversalFarskipPatch
    {
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return typeof(CompAbilityEffect_Farskip).GetMethod(nameof(CompAbilityEffect_Farskip.WorldMapExtraLabel));
            yield return typeof(CompAbilityEffect_Farskip).GetMethod(nameof(CompAbilityEffect_Farskip.Valid), [typeof(GlobalTargetInfo), typeof(bool)]);
            if (ModsConfig.IsActive("VanillaExpanded.VPsycastsE"))
            {
                Log.Message("[FarskipImmobile] Patching VPE as well");
                yield return AccessTools.Method("VanillaPsycastsExpanded.Skipmaster.Ability_WorldTeleport:CanHitTargetTile");
                yield return AccessTools.Method("VanillaPsycastsExpanded.Skipmaster.Ability_WorldTeleport:IsEnabledForPawn");
            }
        }
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var list = instructions.ToList();
            var targetMethodIns = AccessTools.PropertyGetter(typeof(Caravan), nameof(Caravan.ImmobilizedByMass));
            while (list.Any(x => x.Calls(targetMethodIns)))
            {
                var index = list.FirstIndexOf(x => x.Calls(targetMethodIns));
                list[index] = new CodeInstruction(OpCodes.Ldc_I4_0);
                list.RemoveAt(index - 1);
            }
            return list;
        }
    }
}