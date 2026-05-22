using Aquarium.AquariumCode.Character;
using Aquarium.AquariumCode.Relics;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Relics;


[Pool(typeof(AquariumRelicPool))]
public class ScopeAttachment() : AquariumRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Rare;

    private const string _extraDamageKey = "ExtraDamage";

   

   
protected override IEnumerable<DynamicVar> CanonicalVars => [   new DynamicVar("ExtraDamage", 3M) ];
    public override Decimal ModifyDamageAdditive(
        Creature? target,
        Decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        return !props.IsPoweredAttack() || cardSource == null || !cardSource.Keywords.Contains(CardCmdPatches.Weapon)|| dealer != this.Owner.Creature && cardSource.Owner != this.Owner ? 0M : this.DynamicVars["ExtraDamage"].BaseValue;
    }
}