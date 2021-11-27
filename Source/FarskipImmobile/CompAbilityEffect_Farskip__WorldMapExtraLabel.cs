using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace FarskipImmobile
{
	[HarmonyPatch(typeof(CompAbilityEffect_Farskip), "WorldMapExtraLabel")]
	public class CompAbilityEffect_Farskip_WorldMapExtraLabel
	{
		[HarmonyPostfix]
		private static void Postfix(CompAbilityEffect_Farskip __instance, GlobalTargetInfo target, ref string __result)
		{
			Caravan caravan = __instance.parent.pawn.GetCaravan();

			if(caravan != null && caravan.ImmobilizedByMass)
            {
				var method_ShouldJoinCaravan = __instance.GetType().GetMethod("ShouldJoinCaravan", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
				bool var_ShouldJoinCaravan = (bool)method_ShouldJoinCaravan.Invoke(__instance, new object[] { target });

				if (!__instance.Valid(target, false))
				{
					__result = "AbilityNeedAllyToSkip".Translate();
				}
				if (var_ShouldJoinCaravan)
				{
					__result = "AbilitySkipToJoinCaravan".Translate();
				}
				__result = "AbilitySkipToRandomAlly".Translate();
			}
		}
	}
}