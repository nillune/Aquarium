using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Powers;

  
public class GlowInTheDarkPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] { HoverTipFactory.ForEnergy(this) };
    }
    /*
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            return (IEnumerable<IHoverTip>) new <IHoverTip>(HoverTipFactory.ForEnergy((PowerModel) this));
        }
    } */

    public override Decimal ModifyMaxEnergy(Player player, Decimal amount)
    {
        return player != this.Owner.Player ? amount : amount + (Decimal) this.Amount;
    }
}