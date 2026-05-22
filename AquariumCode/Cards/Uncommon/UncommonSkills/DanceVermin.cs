using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  /*
public class DanceVermin() : AquariumCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardKeyword.Exhaust ];
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new PowerVar<IntangiblePower>(1), new DynamicVar("Power",20)];
 
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        DanceVermin cardSource = this;
        await PowerCmd.Apply<IntangiblePower>(
            Owner.Creature,
            DynamicVars[nameof(IntangiblePower)].BaseValue,
            Owner.Creature,
            this);
        DanceVerminPower danceVerminPower = await PowerCmd.Apply<DanceVerminPower>(cardSource.Owner.Creature, cardSource.DynamicVars["Power"].BaseValue ,
            cardSource.Owner.Creature, (CardModel)cardSource);
    }

    protected override void OnUpgrade() => this.DynamicVars["Power"].UpgradeValueBy(-5M);
}
*/