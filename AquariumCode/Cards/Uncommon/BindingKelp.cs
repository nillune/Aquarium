using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class BindingKelp() : AquariumCard(1,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new DamageVar(10, ValueProp.Move), new PowerVar<VulnerablePower>(1M), new PowerVar<WeakPower>(1M)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        BindingKelp bindingKelp = this;
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .TargetingAllOpponents(CombatState)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);

        foreach (Creature enemy in CombatState.HittableEnemies)
        {
            WeakPower weakPower = await PowerCmd.Apply<WeakPower>(enemy,
                bindingKelp.DynamicVars.Weak.BaseValue, bindingKelp.Owner.Creature, (CardModel)bindingKelp);
            VulnerablePower VulnerablePower = await PowerCmd.Apply<VulnerablePower>(enemy,
                bindingKelp.DynamicVars.Weak.BaseValue, bindingKelp.Owner.Creature, (CardModel)bindingKelp);
        }
    }

    protected override void OnUpgrade()
    {
    
       
        DynamicVars["WeakPower"].UpgradeValueBy(1m);
        DynamicVars["VulnerablePower"].UpgradeValueBy(1m);
    }
}