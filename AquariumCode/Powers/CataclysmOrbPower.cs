using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;


namespace Aquarium.AquariumCode.Powers;

 
 
public class CataclysmOrbPower : CustomPowerModel
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
        VigorPower vigorPower = await PowerCmd.Apply<VigorPower>(
            this.Owner,
            this.Amount,
            this.Owner,
            (CardModel) null);
        this.Flash();
        int num = await PowerCmd.ModifyAmount((PowerModel) this, +1, (Creature) null, (CardModel) null);
        //await PowerCmd.Remove((PowerModel)power);
    }
}