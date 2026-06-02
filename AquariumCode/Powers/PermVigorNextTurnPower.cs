// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Powers.SummonNextTurnPower
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 623673A3-2F6A-4E15-A560-4F44F2297867
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll

using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

#nullable enable
namespace Aquarium.AquariumCode.Powers;
  
  
public sealed class PermVigorNextTurnPower : CustomPowerModel
{

   


    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;



    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {

        if (player != this.Owner.Player || this.AmountOnTurnStart == 0)
            return;
        VigorPower vigorPower = await PowerCmd.Apply<VigorPower>(
            new ThrowingPlayerChoiceContext(), this.Owner,
            this.Amount,
            this.Owner,
            (CardModel)null);
        this.Flash();
    }

    public override string CustomPackedIconPath
    {
        get
        {
            var path = "res://Aquarium/images/powers/big/perm_vigor_next_turn_power.png";
            
            return ResourceLoader.Exists(path) ? path : "power.png".PowerImagePath();
        }
    }
//  why the HELL did i have to do this manually?? hello?????????????????????
    public override string CustomBigIconPath
    {
        get
        {
            var path = "res://Aquarium/images/powers/big/perm_vigor_next_turn_power.png";
           
            return ResourceLoader.Exists(path) ? path : "power.png".BigPowerImagePath();
        }
    }
    
}