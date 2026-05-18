using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;


namespace Aquarium.AquariumCode.Powers;

  
public class GlowInTheDarkPower : CustomPowerModel
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