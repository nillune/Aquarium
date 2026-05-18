using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Powers;


public class DanceVerminPower : CustomPowerModel
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
    private int TurnsPassed = 0;
    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {  
     
        DanceVerminPower power = this;
        if (side != power.Owner.Side)
            return;
        TurnsPassed++;
        if (TurnsPassed == 2)
        {
            DamageVar dynamicVar = (DamageVar) power.DynamicVars["SelfDamage"];
            IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage(choiceContext, power.Owner, power.Amount, dynamicVar.Props, power.Owner, (CardModel) null);
            power.Flash();
            await PowerCmd.Remove((PowerModel)power);
        }
    }
}