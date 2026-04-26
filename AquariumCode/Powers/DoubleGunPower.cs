using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;


namespace Aquarium.AquariumCode.Powers;

#nullable enable
public class DoubleGunPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    {
        DoubleGunPower doubleGunPower = this;
        if (!cardPlay.Card.Keywords.Contains(CardCmdPatches.Weapon))
            return;
            
        //CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(cardPlay.Card, PileType.Hand, true);
        CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(cardPlay.Card.CreateClone(), PileType.Hand, true);
        DoubleGunPower power = this;
        
        await PowerCmd.Remove((PowerModel) power);
    }
    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        DoubleGunPower power = this;
        await PowerCmd.Remove((PowerModel) power);
    }
}