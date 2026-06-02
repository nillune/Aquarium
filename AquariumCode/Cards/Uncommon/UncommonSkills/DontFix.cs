using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class DontFix() : AquariumCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("Power",1), new CardsVar(2)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        DontFix dontFix = this;
        DontFix cardSource = this;
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.IntValue, Owner);
        DontFixPower dontFixPower = await PowerCmd.Apply<DontFixPower>(choiceContext, cardSource.Owner.Creature, cardSource.DynamicVars["Power"].BaseValue ,
            cardSource.Owner.Creature, (CardModel)cardSource);
    }

    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
        //this.DynamicVars["Power"].UpgradeValueBy(2M);
    }
}