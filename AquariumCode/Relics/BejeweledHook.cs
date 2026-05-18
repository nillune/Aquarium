using Aquarium.AquariumCode.Character;
using Aquarium.AquariumCode.Relics;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Enchantments;

namespace Aquarium.AquariumCode.Relics;

  
[Pool(typeof(AquariumRelicPool))]
public class BejeweledHook() : AquariumRelic
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("Vigorous", 5M)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => HoverTipFactory.FromEnchantment<Momentum>(this.DynamicVars["Vigorous"].IntValue);
    }
  
    public override RelicRarity Rarity =>
        RelicRarity.Shop;
    public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
    {
      
        options.Add((RestSiteOption) new LureRestSiteOption(player));
        return true;
    }
}
    
