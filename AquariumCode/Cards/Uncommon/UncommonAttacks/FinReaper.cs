using Aquarium.AquariumCode.Cards;
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


public class FinReaper() : AquariumCard(1,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(4, ValueProp.Move), new RepeatVar(2)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] { HoverTipFactory.FromPower<DoomPower>() };
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        FinReaper finReaper = this;
        DoomPower doomPower = await PowerCmd.Apply<DoomPower>(choiceContext, play.Target,
            (Decimal)(await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).FromCard((CardModel)this)
                .Targeting(play.Target)
                .WithHitCount(DynamicVars.Repeat.IntValue)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext)).Results
            .SelectMany<List<DamageResult>, DamageResult>(
                (Func<List<DamageResult>, IEnumerable<DamageResult>>)(r => (IEnumerable<DamageResult>)r))
            .Sum<DamageResult>((Func<DamageResult, int>)(r => r.TotalDamage)), Owner.Creature, (CardModel)this);
    }






    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
    }
}