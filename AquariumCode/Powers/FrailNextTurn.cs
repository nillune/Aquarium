using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Powers;

 
public class FrailNextTurn : CustomPowerModel
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

   

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
       
        if (player != this.Owner.Player || this.AmountOnTurnStart == 0)
            return;
        FrailNextTurn frailNextTurn = await PowerCmd.Apply<FrailNextTurn>(
            new ThrowingPlayerChoiceContext(),   this.Owner,
            this.Amount,
            this.Owner,
            (CardModel) null);
        this.Flash();
        await PowerCmd.Remove((PowerModel)this);
    }
}