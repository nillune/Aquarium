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
using MegaCrit.Sts2.Core.Models.Cards;
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
    // ReSharper disable once InconsistentNaming
    static CardModel[] PreviousCards = {null!, null!};
    private int playedWeapons;
    
    //lolll you figured out my secret and that I just added weapon into aquarium starting relics LOL. originally it wasn't but it was buggy so i just moved it here.
    //though dw it still works as other classes, just a bit more buggy.
    public override async Task AfterCardExhausted(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool _)
    {
        DecroratedBowl decroratedBowl = this;
        if (decroratedBowl.Owner != card.Owner)
            return;
        if (playedWeapons >= 11)
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
       
           
        if (card.Keywords.Contains(CardKeyword.Ethereal))
            card.AddKeyword(CardKeyword.Exhaust);
        card.ExhaustOnNextPlay = true;
       
        await CardCmd.AutoPlay(choiceContext, card, (Creature) null);
     
        // Autoplay the card
        //TaskHelper.RunSafely(CardCmd.AutoPlay(choiceContext, card, (Creature)null, AutoPlayType.Default));
    }

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        playedWeapons = 0;
        PreviousCards[0] = null!;
        PreviousCards[1] = null!;
    }
    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    { 
     if (cardPlay.Card != PreviousCards[0])
         PreviousCards[0] = null!;
     if (cardPlay.Card != PreviousCards[1])
         PreviousCards[1] = null!;
    }

}
