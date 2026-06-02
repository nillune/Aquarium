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

    
    //lolll you figured out my secret and that I just added weapon into aquarium starting relics LOL. originally it wasn't but it was buggy so i just moved it here.
    //though dw it still works as other classes, just a bit more buggy.
    private bool RetainFilter(CardModel card) => !card.ShouldRetainThisTurn;
   
    static CardModel[] PreviousCards = {null!, null!};
    private int playedWeapons;
    public override async Task AfterCardExhausted(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool _)
    {
        LuxuryBowl luxuryBowl = this;
        if (luxuryBowl.Owner != card.Owner)
            return;
        if (playedWeapons >= 20)
            return;
        playedWeapons++;
        //MainFile.Logger.Info(  _PreviousCard + "exhaust");
        // Only autoplay if the card has the Weapon keyword
        if (!card.Keywords.Contains(CardCmdPatches.Weapon))
            return;
        if (PreviousCards[0] == card)
            return;
        if (PreviousCards[1] == card)
            return;
        PreviousCards[1] = PreviousCards[0];
        PreviousCards[0] = card;
       
           
        
        card.ExhaustOnNextPlay = true;
      
        await CardCmd.AutoPlay(choiceContext, card, (Creature) null);
     
        // Autoplay the card
        //TaskHelper.RunSafely(CardCmd.AutoPlay(choiceContext, card, (Creature)null, AutoPlayType.Default));
    }

  
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
       
        PreviousCards[0] = null;
        PreviousCards[1] = null;
        playedWeapons = 0;
    }
    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (cardPlay.Card.Keywords.Contains(CardCmdPatches.Weapon))
            return;
        if (cardPlay.Card != PreviousCards[0])
            PreviousCards[0] = null;
        if (cardPlay.Card != PreviousCards[1])
            PreviousCards[1] = null;
    }
}