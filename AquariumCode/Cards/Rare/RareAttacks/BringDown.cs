using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Rare;

 
public class BringDown() : AquariumCard(0,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(7, ValueProp.Move)];
    protected override bool HasEnergyCostX => true;
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
     
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        int hitCount = this.ResolveEnergyXValue();
        if (this.Owner.Creature.HasPower<FrailPower>())
            hitCount *= 2;
        AttackCommand attackCommand = await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).WithHitCount(hitCount).FromCard((CardModel) this).Targeting(play.Target).WithHitFx("vfx/vfx_giant_horizontal_slash", tmpSfx: "slash_attack.mp3").Execute(choiceContext);
    
    }

    protected override void OnUpgrade()
    {
        this.AddKeyword(CardKeyword.Retain);
    }
}