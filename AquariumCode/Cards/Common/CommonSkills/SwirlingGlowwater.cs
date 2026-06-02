using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Common;

  
public class SwirlingGlowwater() : AquariumCard(0,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1), new PowerVar<FrailPower>(1)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {    
             HoverTipFactory.FromPower<FrailPower>(), HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
        };
    } public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
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
        await PowerCmd.Apply<FrailPower>(
            choiceContext,  Owner.Creature,
            DynamicVars[nameof(FrailPower)].BaseValue,
            Owner.Creature,
            this);
        if (source.IsUpgraded)
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.IntValue, source.Owner);
        
   
        
    }

}




    
