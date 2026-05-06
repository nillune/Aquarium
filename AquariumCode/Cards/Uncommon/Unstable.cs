using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.DevConsole.ConsoleCommands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class Unstable() : AquariumCard(1,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new DamageVar(5, ValueProp.Move),  (DynamicVar) new PowerVar<VulnerablePower>(1M) ];


    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Unstable unstable = this;
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .TargetingAllOpponents(CombatState)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);

        foreach (Creature enemy in CombatState.HittableEnemies)
        {
            VulnerablePower vulnerablePower = await PowerCmd.Apply<VulnerablePower>(enemy,
                unstable.DynamicVars.Weak.BaseValue, unstable.Owner.Creature, (CardModel)unstable);
        }
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(3M);
    }

    public override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
    {
        Unstable unstable = this;
        if (this.Owner.Creature.HasPower<FrailPower>())
        {
            unstable.AddKeyword(CardKeyword.Retain);
        }
        else
        {
            unstable.RemoveKeyword(CardKeyword.Retain);
        }
    }
}