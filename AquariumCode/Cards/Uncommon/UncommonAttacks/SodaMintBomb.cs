using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class SodaMintBomb() : AquariumCard(2,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromPower<FrailPower>(), HoverTipFactory.FromPower<StrengthPower>()};
    }
    protected override IEnumerable<DynamicVar> CanonicalVars => [(DynamicVar) new DamageVar(13M, ValueProp.Move),
        new DynamicVar("StrengthLoss", 10M), new EnergyVar(1)];
    protected override bool ShouldGlowGoldInternal
    {
        get
        {
            return this.Owner.Creature.HasPower<FrailPower>();
        }
    }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        /*
        if (this.Owner.Creature.HasPower<FrailPower>())
        {
            SodaMintBombPower sodaMintBombPower = await PowerCmd.Apply<SodaMintBombPower>(choiceContext, play.Target, this.DynamicVars["StrengthLoss"].BaseValue*-1, this.Owner.Creature, (CardModel) this);
        }    
        */
    }
    public override async Task AfterPowerAmountChanged(
        PlayerChoiceContext choiceContext,
        PowerModel power,
        Decimal amount,
        Creature? applier,
        CardModel? cardSource)
    {
       
        if (amount <= 0M || applier != this.Owner.Creature || !(power is FrailPower))
            return;
       
        this.EnergyCost.SetThisTurn(0);
    }
    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(6m);
    }
}