using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Powers;

  
 
public class AttentionToDetailPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override Decimal ModifyHandDraw(Player player, Decimal count)
    {
        return player != this.Owner.Player ? count : Math.Max(0M, count - (Decimal) 1);
    }

    public override Task AfterModifyingHandDraw()
    {
        this.Flash();
        return Task.CompletedTask;
    }
    public override Decimal ModifyMaxEnergy(Player player, Decimal amount)
    {
        return player != this.Owner.Player ? amount : amount + (Decimal) this.Amount;
    }
}