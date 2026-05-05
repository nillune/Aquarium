using Aquarium.AquariumCode.Character;
using Aquarium.AquariumCode.Extensions;
using Aquarium.AquariumCode.Relics;
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
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Relics;

 
[Pool(typeof(AquariumRelicPool))]
public class DecroratedBowl() : AquariumRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;
    
    public override RelicModel GetUpgradeReplacement()
    {
        return ModelDb.Relic<LuxuryBowl>();
    }
    public override async Task BeforeFlushLate(PlayerChoiceContext choiceContext, Player player)
    {
        DecroratedBowl source = this;
        DecroratedBowl decroratedBowl = this;
        if (player != source.Owner || !Hook.ShouldFlush(player.Creature.CombatState, player))
            return;
        CardPile pile = PileType.Hand.GetPile(source.Owner);
        CardModel card = Owner.RunState.Rng.CombatCardSelection.NextItem<CardModel>((IEnumerable<CardModel>) pile.Cards);
        if (card == null)
            return;
            card.GiveSingleTurnRetain();
            decroratedBowl.Flash();
    }
    static CardModel _PreviousCard;
  
    public override async Task AfterCardExhausted(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool _)
    {
        DecroratedBowl decroratedBowl = this;
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
        //decroratedBowl.AutoplayingCards.Add(card);
        await CardCmd.AutoPlay(choiceContext, card, (Creature) null);
        //decroratedBowl.AutoplayingCards.Remove(card);
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
