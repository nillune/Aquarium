using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Rare;

 
public class Carried() : AquariumCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.AllAllies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new PowerVar<FrailPower>(2),
        new PowerVar<DexterityPower>(1)];

    //protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Strike };
    public override CardMultiplayerConstraint MultiplayerConstraint  => CardMultiplayerConstraint.MultiplayerOnly;
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Carried carried = this;
        foreach (Creature creature in carried.CombatState.GetTeammatesOf(carried.Owner.Creature).Where<Creature>((Func<Creature, bool>) (c => c != null && c.IsAlive && !c.IsPlayer)))
          
            await PowerCmd.Apply<DexterityPower>(
                creature,
                DynamicVars[nameof(DexterityPower)].BaseValue,
                Owner.Creature,
                this);
            //await PlayerCmd.GainEnergy((Decimal) carried.DynamicVars.Energy.IntValue, creature.Player);
            await PowerCmd.Apply<FrailPower>(
                Owner.Creature,
                DynamicVars[nameof(FrailPower)].BaseValue,
                Owner.Creature,
                this);
    }   

    protected override void OnUpgrade()
    {
        DynamicVars["DexterityPower"].UpgradeValueBy(1m);
    }
}