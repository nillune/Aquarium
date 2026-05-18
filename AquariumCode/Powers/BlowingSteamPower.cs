using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Powers;

  
public class BlowingSteamPower : CustomPowerModel
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
        get => new[] {   HoverTipFactory.FromPower<VigorPower>()};
    }
    public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        object blowingSteamPower;
        if (side != CombatSide.Player)
            return;
        IReadOnlyList<Creature> hittableEnemies = this.CombatState.HittableEnemies;
        if (hittableEnemies.Count == 0)
            return;
        Creature target = this.Owner.Player.RunState.Rng.CombatTargets.NextItem<Creature>((IEnumerable<Creature>) hittableEnemies);
        if (this.Owner.GetPowerAmount<VigorPower>() > 0)
        {
             int calcDamage = (this.Owner.GetPowerAmount<VigorPower>() / 5 * this.Amount);
             this.Flash();
             IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), target, (Decimal) calcDamage, ValueProp.Unpowered, this.Owner, (CardModel) null);

        }

        
    }

}