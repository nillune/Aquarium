using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Powers;

 
public class GlassToTreasurePower : CustomPowerModel
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

public override async Task AfterPowerAmountChanged(
    PlayerChoiceContext choiceContext,
    PowerModel power,
    Decimal amount,
    Creature? applier,
    CardModel? cardSource)
{
       
    if (amount <= 0M || applier != this.Owner || !(power is FrailPower))
        return;
   // GlassToTreasurePower lassToTreasurePower = this;
    
    this.Flash();
    for (int i = 0; i < this.Amount; ++i)
        await OrbCmd.Channel((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), OrbModel.GetRandomOrb(this.Owner.Player.RunState.Rng.CombatOrbGeneration).ToMutable(), this.Owner.Player);
}
}