using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.UI;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Uncommon;

 
public class BoxerJellies() : AquariumCard(2,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromPower<VulnerablePower>(), HoverTipFactory.FromPower<WeakPower>()};
    }
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new DamageVar(3, ValueProp.Move), new RepeatVar(3), new PowerVar<WeakPower>(2), 
        new PowerVar<VulnerablePower>(2)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardCmdPatches.Weapon ];
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitCount(DynamicVars.Repeat.IntValue)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        if (!play.Target.HasPower<VulnerablePower>() ){
                await PowerCmd.Apply<VulnerablePower>(
                    choiceContext, play.Target,
            DynamicVars[nameof(VulnerablePower)].BaseValue,
            Owner.Creature,
            this);
        } 
        if (!play.Target.HasPower<WeakPower>() ){
            await PowerCmd.Apply<WeakPower>(
                choiceContext, play.Target,
                DynamicVars[nameof(WeakPower)].BaseValue,
                Owner.Creature,
                this);
        } 
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Repeat.UpgradeValueBy(1m);
        DynamicVars["WeakPower"].UpgradeValueBy(1m);
        DynamicVars["VulnerablePower"].UpgradeValueBy(1m);
    }
}