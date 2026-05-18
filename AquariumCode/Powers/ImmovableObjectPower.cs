using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
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

 
public class ImmovableObjectPower : CustomPowerModel
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
    public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Player || !this.Owner.HasPower<FrailPower>())
            return;
        Decimal num = await CreatureCmd.GainBlock(this.Owner, (Decimal)this.Owner.Block,
            ValueProp.Unpowered | ValueProp.Move, null);
    }
}
    
    
