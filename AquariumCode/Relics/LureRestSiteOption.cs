// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Entities.RestSite.LiftRestSiteOption
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 623673A3-2F6A-4E15-A560-4F44F2297867
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll

#nullable enable
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.RestSite;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;

namespace Aquarium.AquariumCode.Relics;

public class LureRestSiteOption(Player owner) : RestSiteOption(owner)
{
  public override LocString Description
  {
    get
    {
      LocString description = base.Description;

      description.Add("Lure", (Decimal)0);
      return description;
    }
  }

  public override string OptionId => "LURE";

  public override async Task<bool> OnSelect()
  {
    CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
    Vigorous canonicalVigorous = ModelDb.Enchantment<Vigorous>();
    foreach (CardModel card in await CardSelectCmd.FromDeckForEnchantment(Owner, (EnchantmentModel)canonicalVigorous, this.Owner.GetRelic<BejeweledHook>().DynamicVars["Vigorous"].IntValue,
               prefs))
    {
      CardCmd.Enchant(canonicalVigorous.ToMutable(), card, (Decimal)this.Owner.GetRelic<BejeweledHook>().DynamicVars["Vigorous"].IntValue);
      CardCmd.Preview(card);
    }
    canonicalVigorous = (Vigorous)null;
    return await Task.FromResult<bool>(true);
  }

  public virtual string CustomIconPath => "res://Aquarium/images/ui/rest_site/option_lure.png";
  [HarmonyPatch(typeof(RestSiteOption), "IconPath", MethodType.Getter)]
internal class CustomRestSiteOptionIconPath
{
    [HarmonyPrefix]
    private static bool Custom(RestSiteOption __instance, ref string __result)
    {
        if (__instance is not LureRestSiteOption { CustomIconPath: { } path })
            return true;
        __result = path;
        return false;
    }
}

  public override Task DoLocalPostSelectVfx(CancellationToken ct = default (CancellationToken))
  {
    NGame.Instance?.ScreenShake(ShakeStrength.Strong, ShakeDuration.Short);
    return Task.CompletedTask;
  }

  public override Task DoRemotePostSelectVfx()
  {
    NRestSiteRoom instance = NRestSiteRoom.Instance;
    NRestSiteCharacter parent = instance != null ? instance.Characters.First<NRestSiteCharacter>((Func<NRestSiteCharacter, bool>) (c => c.Player == this.Owner)) : (NRestSiteCharacter) null;
    parent?.Shake();
    NRelicFlashVfx child = NRelicFlashVfx.Create((RelicModel) ModelDb.Relic<BejeweledHook>());
    if (child == null)
      return Task.CompletedTask;
    if (parent != null)
      parent.AddChildSafely((Node) child);
    child.Position = Vector2.Zero;
    return Task.CompletedTask;
  }
  
}
