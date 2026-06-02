using Aquarium.AquariumCode.Character;
using Aquarium.AquariumCode.Relics;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace Aquarium.AquariumCode.Relics;

  
[Pool(typeof(AquariumRelicPool))]
public class CanOfWurms() : AquariumRelic
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [   new PowerVar<VigorPower>(1)];
    public override RelicRarity Rarity =>
        RelicRarity.Common;
    public override Decimal ModifyPowerAmountGivenAdditive(
        PowerModel power,
        Creature giver,
        Decimal amount,
        Creature? target,
        CardModel? cardSource)
    {
        return !(power is VigorPower) || giver != this.Owner.Creature ? amount : amount + (Decimal) DynamicVars[nameof(VigorPower)].BaseValue;
    }

    public override Task AfterModifyingPowerAmountGiven(PowerModel power)
    {
        this.Flash();
        return Task.CompletedTask;
    }
    
}