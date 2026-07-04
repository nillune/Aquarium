using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class ExtraStorage() : AquariumCard(1,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AllAllies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("ExtraStorage",1)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardKeyword.Exhaust ];
    public override CardMultiplayerConstraint MultiplayerConstraint  => CardMultiplayerConstraint.MultiplayerOnly;
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        foreach (Creature creature in this.CombatState.GetTeammatesOf(this.Owner.Creature)
                     .Where<Creature>((Func<Creature, bool>)(c => c != null && c.IsAlive && c.IsPlayer)))
        {
            RetainHandPower retainHandPower = await PowerCmd.Apply<RetainHandPower>(choiceContext, creature,
                this.DynamicVars[nameof(ExtraStorage)].BaseValue, this.Owner.Creature, (CardModel)this);
        }

    }

    protected override void OnUpgrade()
    {
    this.RemoveKeyword(CardKeyword.Exhaust);
    }
}