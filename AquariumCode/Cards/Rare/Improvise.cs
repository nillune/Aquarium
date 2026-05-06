using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;

namespace Aquarium.AquariumCode.Cards.Rare;

 
public class Improvise() : AquariumCard(2,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("CardSelect",1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
       
        Improvise improvise = this;
        
        CardSelectorPrefs prefs = new CardSelectorPrefs(improvise.SelectionScreenPrompt, Decimal.ToInt32(this.DynamicVars["CardSelect"].BaseValue));
        CardModel card = (await CardSelectCmd.FromHand(choiceContext, improvise.Owner, prefs, null, (AbstractModel) improvise)).FirstOrDefault<CardModel>();
        if (card == null)
            return;
        CardCmd.ApplyKeyword(card, CardKeyword.Exhaust);
    }

    protected override void OnUpgrade() => this.DynamicVars["CardSelect"].UpgradeValueBy(1M);
}