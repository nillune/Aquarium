using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using System;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Powers;

  
public class FoolproofPower : PowerModel
{
    
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;
    
    private bool AttackPlayedThisTurn = true; 
    private int CardsPlayedThisTurn = 1; 
    private bool AttackPlayedLastTurn = true; 
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Type == CardType.Attack)
            AttackPlayedThisTurn = true;
        CardsPlayedThisTurn = CardsPlayedThisTurn + 1;
        
    }
  
    public override async Task BeforeHandDraw(
        Player player,
        PlayerChoiceContext choiceContext,
        CombatState combatState)
    {
        CardsPlayedThisTurn = 0;
        if (!AttackPlayedThisTurn)
            AttackPlayedLastTurn = false;
        AttackPlayedThisTurn = false;

    }

    public override bool TryModifyEnergyCostInCombat(
        CardModel card,
        Decimal originalCost,
        out Decimal modifiedCost)
    {
        modifiedCost = originalCost;
       
            if (this.ShouldSkip(card) || AttackPlayedLastTurn)
                return false;
        

        modifiedCost = 0M;
        return true;
    }

    public override bool TryModifyStarCost(
        CardModel card,
        Decimal originalCost,
        out Decimal modifiedCost)
    {
        modifiedCost = originalCost;
        if (this.ShouldSkip(card)  || AttackPlayedLastTurn )
        
            return false;
        modifiedCost = 0M;
        return true;
    }


    private bool ShouldSkip(CardModel card)
    {
        bool flag1 = card.Owner.Creature != this.Owner;
        if (!flag1)
        {
            PileType? type = card.Pile?.Type;
            bool flag2;
            if (type.HasValue)
            {
                switch (type.GetValueOrDefault())
                {
                    case PileType.Hand:
                    case PileType.Play:
                        flag2 = true;
                        goto label_5;
                }
            }
            flag2 = false;
            label_5:
            flag1 = !flag2;
        }
        return flag1 || CardsPlayedThisTurn >= this.Amount ;
    }


}