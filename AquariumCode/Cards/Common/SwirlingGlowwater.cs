using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Cards.Common;

  
public class SwirlingGlowwater() : AquariumCard(0,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        SwirlingGlowwater source = this;
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1);
        CardModel card = (await CardSelectCmd.FromHand(choiceContext, source.Owner, prefs, (Func<CardModel, bool>) null, (AbstractModel) source)).FirstOrDefault<CardModel>();
        if (card == null)
            return;
        await CardCmd.Exhaust(choiceContext, card);
        if (source.IsUpgraded)
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.IntValue, source.Owner);
    }

    
}