using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Rare;

 
public class ReelBack() : AquariumCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromKeyword(CardKeyword.Exhaust)};
    }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ReelBack card1 = this;
        CardSelectorPrefs prefs = new CardSelectorPrefs(card1.SelectionScreenPrompt, 1);
        CardModel card2 = (await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Exhaust.GetPile(card1.Owner).Cards, card1.Owner, prefs)).FirstOrDefault<CardModel>();
        if (card2 == null)
            return;
        await CardCmd.Exhaust(choiceContext, card2);
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}