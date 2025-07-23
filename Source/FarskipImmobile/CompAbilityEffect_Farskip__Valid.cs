using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace FarskipImmobile
{
	[HarmonyPatch(typeof(CompAbilityEffect_Farskip), "Valid")]
	public static class CompAbilityEffect_Farskip_Valid
	{
		public static void Postfix(CompAbilityEffect_Farskip __instance, GlobalTargetInfo target, bool throwMessages, ref bool __result)
		{

			Caravan caravan = __instance.parent.pawn.GetCaravan();
			Caravan caravan2 = target.WorldObject as Caravan;

			if (__result == true || caravan == null || caravan == caravan2)
			{
				return;
			}
			if(__instance.ShouldEnterMap(target) || __instance.ShouldJoinCaravan(target))
            {
				__result = true;
            }
		}
	}
}