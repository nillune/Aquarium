using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Powers;

  
  
public class HurricaneFormPower : PowerModel
{
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