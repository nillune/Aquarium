

using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models.Badges;

#nullable enable
namespace MegaCrit.Sts2.Core.Models.Powers;

  
  
public sealed class AnchovyPower : PowerModel
{
   
  
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    private int AnnoyingVigor = 0;
   
    public override async Task AfterCardDrawn(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool fromHandDraw)
    {
       
        AnchovyPower power = this;
        if (power.Owner.GetPowerAmount<VigorPower>() != 0)
        {
            AnnoyingVigor = power.Owner.GetPowerAmount<VigorPower>();
        }
      
        //GD.Print(AnnoyingVigor, "first");
        AnchovyPower anchovyPower = this;
        VigorPower vigorPower = await PowerCmd.Apply<VigorPower>(
            power.Owner,
            -power.Owner.GetPowerAmount<VigorPower>(),
            power.Owner,
            (CardModel) null);
      
      
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        AnchovyPower power = this;
        AnchovyPower anchovyPower = this;
        if (player != anchovyPower.Owner.Player || anchovyPower.AmountOnTurnStart == 0)
        {
            return;
        }
           
        // GD.Print(AnnoyingVigor, "first");
        VigorPower vigorPower = await PowerCmd.Apply<VigorPower>(
            power.Owner,
            AnnoyingVigor,
            power.Owner,
            (CardModel) null);
        AnnoyingVigor = 0;
        await PowerCmd.Remove((PowerModel)power);
    }

    public int BearingVigor;
    
}