using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;


namespace Aquarium.AquariumCode.Powers;

  
  
public class HurricaneFormPower : CustomPowerModel
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

    public override int ModifyAttackHitCount(AttackCommand attack, int hitCount)
    {
        if (attack.Attacker == this.Owner)
        {
            this.Flash();
            return hitCount + this.Amount;
        }

        return hitCount;
    }
}