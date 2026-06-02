using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Powers;


  
public class BootstrapPower : CustomPowerModel
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

    public override PowerStackType StackType => PowerStackType.Single;


    private decimal RealVigor;



    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    {
        BootstrapPower power = this;
        if (cardPlay.Card.Owner.Creature != this.Owner)
            return;
        RealVigor = power.Owner.GetPowerAmount<VigorPower>();
      
    }
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        BootstrapPower power = this;
        
        if (cardPlay.Card.Owner.Creature != this.Owner || cardPlay.Card.Type != CardType.Attack)
            return;

        await PowerCmd.Apply<VigorPower>(
            new ThrowingPlayerChoiceContext(), power.Owner,
                RealVigor,
                power.Owner,
                (CardModel)null);
        power.Flash();
        // RealVigor = power.Owner.GetPowerAmount<VigorPower>();
        
    }

}