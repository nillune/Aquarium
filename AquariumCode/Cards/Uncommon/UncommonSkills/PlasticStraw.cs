using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
 
public class PlasticStraw() : AquariumCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardKeyword.Exhaust ];
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new PowerVar<FrailPower>(7)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromPower<FrailPower>()};
    }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<FrailPower>(
            choiceContext,   Owner.Creature,
            DynamicVars[nameof(FrailPower)].BaseValue,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade() => this.AddKeyword(CardKeyword.Retain);
}
