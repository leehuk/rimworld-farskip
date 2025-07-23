using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace FarskipImmobile
{
	[HarmonyPatch(typeof(CompAbilityEffect_Farskip), "WorldMapExtraLabel")]
	public static class CompAbilityEffect_Farskip_WorldMapExtraLabel
	{
		public static void Postfix(CompAbilityEffect_Farskip __instance, GlobalTargetInfo target, ref string __result)
		{
			Caravan caravan = __instance.parent.pawn.GetCaravan();

			if(caravan != null && caravan.ImmobilizedByMass)
            {
				if (!__instance.Valid(target, false))
				{
					__result = "AbilityNeedAllyToSkip".Translate();
				}
				if (__instance.ShouldJoinCaravan(target))
				{
					__result = "AbilitySkipToJoinCaravan".Translate();
				}
				__result = "AbilitySkipToRandomAlly".Translate();
			}
		}
	}
}