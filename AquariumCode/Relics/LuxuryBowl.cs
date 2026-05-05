using Aquarium.AquariumCode.Character;
using Aquarium.AquariumCode.Extensions;
using Aquarium.AquariumCode.Relics;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Relics;
  
  
[Pool(typeof(AquariumRelicPool))]
public class LuxuryBowl() : AquariumRelic
{
    
    public override RelicRarity Rarity =>
        RelicRarity.Starter;
    public override async Task BeforeFlushLate(PlayerChoiceContext choiceContext, Player player)
    {
        LuxuryBowl source = this;
        LuxuryBowl luxuryBowl = this;
        if (player != source.Owner || !Hook.ShouldFlush(player.Creature.CombatState, player))
            return;
        CardSelectorPrefs prefs = new CardSelectorPrefs(source.SelectionScreenPrompt, 0, 2);
        List<CardModel> list = (await CardSelectCmd.FromHand(choiceContext, source.Owner, prefs, new Func<CardModel, bool>(RetainFilter), (AbstractModel) source)).ToList<CardModel>();
        if (list.Count == 0)
            return;
        foreach (CardModel cardModel in list)
            cardModel.GiveSingleTurnRetain();
        luxuryBowl.Flash();
    }

   
    private bool RetainFilter(CardModel card) => !card.ShouldRetainThisTurn;
    
    static CardModel _PreviousCard;
  
    public override async Task AfterCardExhausted(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool _)
    {
        LuxuryBowl luxuryBowl = this;
        MainFile.Logger.Info(  _PreviousCard + "exhaust");
        // Only autoplay if the card has the Weapon keyword
        if (!card.Keywords.Contains(CardCmdPatches.Weapon))
            return;
        if (_PreviousCard == card)
            return;
    
        _PreviousCard = card;
       
           
        if (card.Keywords.Contains(CardKeyword.Ethereal))
            card.AddKeyword(CardKeyword.Exhaust);
        card.ExhaustOnNextPlay = true;
        //LuxuryBowl.AutoplayingCards.Add(card);
        await CardCmd.AutoPlay(choiceContext, card, (Creature) null);
        //LuxuryBowl.AutoplayingCards.Remove(card);
        // Autoplay the card
        //TaskHelper.RunSafely(CardCmd.AutoPlay(choiceContext, card, (Creature)null, AutoPlayType.Default));
    }

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
     
        _PreviousCard = null;
    }
    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (cardPlay.Card != _PreviousCard)
            _PreviousCard = null;
    }
}