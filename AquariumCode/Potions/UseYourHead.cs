using Aquarium.AquariumCode.Character;
using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Potions;

[Pool(typeof(AquariumPotionPool))]
public class UseYourHead : CustomPotionModel
{
    public override PotionRarity Rarity => PotionRarity.Rare;
    public override PotionUsage Usage => PotionUsage.CombatOnly;
    public override TargetType TargetType => TargetType.AnyEnemy;
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new PowerVar<FrailPower>(4),new DamageVar(30M, ValueProp.Unpowered) ];
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
    
        PotionModel.AssertValidForTargetedPotion(target);
        DamageVar damage = this.DynamicVars.Damage;
        NCombatRoom instance = NCombatRoom.Instance;
        if (instance != null)
            instance.CombatVfxContainer.AddChildSafely((Node) NGroundFireVfx.Create(target));
        IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage(choiceContext, target, damage.BaseValue, damage.Props, this.Owner.Creature, (CardModel) null, null);
        await PowerCmd.Apply<FrailPower>(
            choiceContext,  this.Owner.Creature,
            DynamicVars[nameof(FrailPower)].BaseValue,
            this.Owner.Creature,
            null);
    }
    public override string? CustomPackedImagePath => "res://Aquarium/images/potions/use_your_head.png";
}