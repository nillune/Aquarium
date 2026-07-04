using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Rare;

 
public class BigHammerhead() : AquariumCard(1,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{ private Decimal _extraDamage;
    private Decimal ExtraDamage
    {
        get => this._extraDamage;
        set
        {
            this.AssertMutable();
            this._extraDamage = value;
        }
    }
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(10, ValueProp.Move), new DynamicVar("Increase", 10)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardCmdPatches.Weapon];
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        AttackCommand attackCommand = await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).FromCard((CardModel) this, play).Targeting(
            play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        Decimal baseValue = this.DynamicVars["Increase"].BaseValue;
        DamageVar damage = this.DynamicVars.Damage;
        damage.BaseValue = damage.BaseValue + baseValue;
        this.EnergyCost.AddThisCombat(1);
        this.ExtraDamage += baseValue;
    }
   
    protected override void OnUpgrade()
    {
        this.DynamicVars["Increase"].UpgradeValueBy(3M);
        DynamicVars.Damage.UpgradeValueBy(3m);
    }
}