using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Entities.Players;


namespace Aquarium.AquariumCode.Powers;

 
public class DragunFishPower : CustomPowerModel
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


    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (this.Owner.GetPowerAmount<VigorPower>() < 3 && this.Owner.GetPowerAmount<VigorPower>() >= 0)
        {
            VigorPower vigorPower2 = await PowerCmd.Apply<VigorPower>(
                new ThrowingPlayerChoiceContext(),   this.Owner,
                -this.Owner.GetPowerAmount<VigorPower>(),
                Owner,
                (CardModel) null);
        }
        VigorPower vigorPower = await PowerCmd.Apply<VigorPower>(
            new ThrowingPlayerChoiceContext(),   this.Owner,
            -Amount,
            Owner,
            (CardModel) null);
        Flash();
    }
    /*
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        DragunFishPower power = this;
        if (cardPlay.Card.Owner.Creature != power.Owner)
            return;
        if (cardPlay.Card.Type != CardType.Attack)
            return;
        if (cardPlay.Card.TargetType != TargetType.AllEnemies)
            return;
        VigorPower vigorPower = await PowerCmd.Apply<VigorPower>(
            power.Owner,
            power.Amount,
            power.Owner,
            (CardModel) null);
        power.Flash();

    }
    */
}