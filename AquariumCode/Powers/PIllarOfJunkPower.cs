using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Powers;

  
public sealed class PillarOfJunkPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task BeforeHandDraw(
        Player player,
        PlayerChoiceContext choiceContext,
        CombatState combatState)
    {
        //PillarOfJunkPower this = this;
        if (player != this.Owner.Player || this.AmountOnTurnStart < 1)
            return;
        
        this.Flash();
        
        // Gain block
        await CreatureCmd.GainBlock(this.Owner, (Decimal)this.Amount, ValueProp.Unpowered, (CardPlay)null);
        
        // Add random status card to hand
        var statusCards = ModelDb.CardPool<StatusCardPool>()
            .GetUnlockedCards(this.Owner.Player.UnlockState, this.Owner.Player.RunState.CardMultiplayerConstraint);
        
        var statusCardsForCombat = CardFactory.GetDistinctForCombat(
            this.Owner.Player,
            statusCards,
            1,
            this.Owner.Player.RunState.Rng.CombatCardGeneration);
        
        await CardPileCmd.AddGeneratedCardsToCombat(statusCardsForCombat, PileType.Hand, true);
    }
}