using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Helpers;

namespace  Aquarium.AquariumCode.Patches;
[HarmonyPatch(typeof(RestSiteOption))]
[HarmonyPatch("IconPath", MethodType.Getter)]
public class RestSiteImagePatch
{
    public static void Postfix(RestSiteOption __instance, ref string __result)
    {
        if (__instance.OptionId == "LURE")
        {
            __result = Path.Join(MainFile.ModId, "images", "ui", "option_lure.png");
        }
       
    }
}