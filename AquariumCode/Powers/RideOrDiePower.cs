
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Models;


#nullable enable
namespace Aquarium.AquariumCode.Powers;

  
  
public sealed class RideOrDiePower : CustomPowerModel
{

    public override string CustomPackedIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
            
            return ResourceLoader.Exists(path) ? path : "power.png".PowerImagePath();
        }
    }

    public override string CustomBigIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
           
            return ResourceLoader.Exists(path) ? path : "power.png".BigPowerImagePath();
        }
    }


    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

   
    public override Decimal ModifyHpLostBeforeOsty(
        Creature target,
        Decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        
        return target == this.Owner || !props.IsPoweredAttack() ? amount = amount + amount : amount = amount;
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        
        RideOrDiePower power = this;
        await PowerCmd.Remove((PowerModel) power);
    }
}