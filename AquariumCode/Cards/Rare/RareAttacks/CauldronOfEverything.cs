using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Rare;

 
public class CauldronOfEverything() : AquariumCard(1,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new DamageVar(6, ValueProp.Move),  new BlockVar(6, ValueProp.Move), 
        new PowerVar<VulnerablePower>(1M), new PowerVar<WeakPower>(1M), new DynamicVar("StrengthLoss", 1M)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {     HoverTipFactory.FromPower<WeakPower>(), HoverTipFactory.Static(StaticHoverTip.Block)
            , HoverTipFactory.FromPower<VulnerablePower>(), HoverTipFactory.FromPower<StrengthPower>()
        };
    }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        await PowerCmd.Apply<WeakPower>(
            choiceContext, play.Target,
            DynamicVars[nameof(WeakPower)].BaseValue,
            Owner.Creature,
            this);
        await PowerCmd.Apply<VulnerablePower>(
            choiceContext, play.Target,
            DynamicVars[nameof(VulnerablePower)].BaseValue,
            Owner.Creature,
            this);
        CauldronOfEverythingPower cauldronOfEverythingPower = await PowerCmd.Apply<CauldronOfEverythingPower>(choiceContext, play.Target,
            this.DynamicVars["StrengthLoss"].BaseValue*-1, this.Owner.Creature, (CardModel)this);
        
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
        DynamicVars.Block.UpgradeValueBy(2m);
        this.DynamicVars["StrengthLoss"].UpgradeValueBy(1M);
    }
}