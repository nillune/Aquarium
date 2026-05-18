using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;


namespace Aquarium.AquariumCode.Powers;

 
public class DontFixPower : CustomPowerModel
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
    private string PreviousCard = null;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
       
        if (cardPlay.Card.Owner.Creature != this.Owner)
            return;
        if (cardPlay.Card.Title == PreviousCard)
        {
            await CreatureCmd.GainBlock(this.Owner, (Decimal)this.Amount, ValueProp.Unpowered, (CardPlay)null);
            this.Flash();
        }

        PreviousCard = cardPlay.Card.Title;



    }
}