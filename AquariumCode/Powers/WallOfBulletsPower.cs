using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;


namespace Aquarium.AquariumCode.Powers;

 
public class WallOfBulletsPower : CustomPowerModel
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

    
    protected override  
    IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] { HoverTipFactory.FromKeyword(CardCmdPatches.Weapon),
            HoverTipFactory.Static(StaticHoverTip.Block) };
    }
   

    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    {
       
        if (cardPlay.Card.Owner != this.Owner.Player || !cardPlay.Card.Keywords.Contains(CardCmdPatches.Weapon))
            return;
        Decimal num = await CreatureCmd.GainBlock(this.Owner, (Decimal) this.Amount, ValueProp.Unpowered, (CardPlay) null);
    }
}