using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Rare;


public class ZephyrOfFlames() : AquariumCard(1,
    CardType.Power, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("Power", 99M)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {     HoverTipFactory.FromPower<ArtifactPower>(), HoverTipFactory.Static(StaticHoverTip.Block)
            , HoverTipFactory.FromPower<VulnerablePower>()
          };
    }
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        ZephyrOfFlames cardSource = this;
        ArgumentNullException.ThrowIfNull((object) cardPlay.Target, "cardPlay.Target");
        await CreatureCmd.TriggerAnim(cardSource.Owner.Creature, "Cast", cardSource.Owner.Character.CastAnimDelay);
        VfxCmd.PlayOnCreatureCenter(cardSource.Owner.Creature, "vfx/vfx_flying_slash");
        int amount = cardSource.DynamicVars["Power"].IntValue;
        await CreatureCmd.LoseBlock(cardPlay.Target, (Decimal) cardPlay.Target.Block);
        if (cardPlay.Target.HasPower<ArtifactPower>())
            await PowerCmd.Remove<ArtifactPower>(cardPlay.Target);
        VulnerablePower vulnerablePower = await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, (Decimal) amount, cardSource.Owner.Creature, (CardModel) cardSource);
        VulnerablePower vulnerablePower2 = await PowerCmd.Apply<VulnerablePower>(cardSource.Owner.Creature, (Decimal) amount, cardSource.Owner.Creature, (CardModel) cardSource);
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}