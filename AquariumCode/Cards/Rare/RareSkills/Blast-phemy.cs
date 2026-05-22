using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Rare;

  
public class Blast_phemy() : AquariumCard(0,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [  new DynamicVar("DamageTurns", 1M)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (this.IsUpgraded)
        {
            TripleDamagePower tripleDamagePower = await PowerCmd.Apply<TripleDamagePower>(this.Owner.Creature, (Decimal) this.DynamicVars["DamageTurns"].BaseValue, this.Owner.Creature, (CardModel) null);

        }
        else
        {
            DoubleDamagePower doubleDamagePower = await PowerCmd.Apply<DoubleDamagePower>(this.Owner.Creature, (Decimal) this.DynamicVars["DamageTurns"].BaseValue, this.Owner.Creature, (CardModel) null);
                
        }

       //NoBlockPower noBlockPower = await PowerCmd.Apply<NoBlockPower>(this.Owner.Creature, this.DynamicVars["NoBlockTurns"].BaseValue, this.Owner.Creature, (CardModel) this);
        Blast_EmphyPower blast_EmphyPower =  await PowerCmd.Apply<Blast_EmphyPower>(this.Owner.Creature,1, this.Owner.Creature, (CardModel) this);
    
    }

    protected override void OnUpgrade()
    {

    }
}