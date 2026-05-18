using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Rare;

 
public class Fatemaker() : AquariumCard(0,
    CardType.Skill, CardRarity.Rare,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8M, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move),
     ];
    protected override bool HasEnergyCostX => true;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int hitCount = this.ResolveEnergyXValue();
        if (this.IsUpgraded)
            ++hitCount;
        ArgumentNullException.ThrowIfNull((object)this.CombatState, "this.CombatState");
        for (int i = 0; i < DynamicVars.Repeat.IntValue; i++)
        {
            IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage(choiceContext, cardPlay.Target,
                this.DynamicVars.Damage, (CardModel)this);
        }
    }

    protected override void OnUpgrade()
    {

    }
}