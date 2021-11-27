using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace FarskipImmobile
{
	[HarmonyPatch(typeof(CompAbilityEffect_Farskip), "Valid")]
	public class CompAbilityEffect_Farskip_Valid
	{
		[HarmonyPostfix]
		private static void Postfix(CompAbilityEffect_Farskip __instance, GlobalTargetInfo target, bool throwMessages, ref bool __result)
		{

			Caravan caravan = __instance.parent.pawn.GetCaravan();
			Caravan caravan2 = target.WorldObject as Caravan;

			if (__result == true || caravan == null || caravan == caravan2)
			{
				return;
			}

			var method_ShouldEnterMap = __instance.GetType().GetMethod("ShouldEnterMap", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			var method_ShouldJoinCaravan = __instance.GetType().GetMethod("ShouldJoinCaravan", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

			bool var_ShouldEnterMap = (bool)method_ShouldEnterMap.Invoke(__instance, new object[] { target });
			bool var_ShouldJoinCaravan = (bool)method_ShouldJoinCaravan.Invoke(__instance, new object[] { target });

			if(var_ShouldEnterMap || var_ShouldJoinCaravan)
            {
				__result = true;
            }
		}
	}
}