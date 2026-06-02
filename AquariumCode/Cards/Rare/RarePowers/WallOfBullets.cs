using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Cards.Rare;

  
public class WallOfBullets() : AquariumCard(2,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    private const string _blockOnExhaustKey = "BlockOnExhaust";
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("BlockOnExhaust", 4M)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.Static(StaticHoverTip.Block)};
    }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        WallOfBulletsPower wallOfBulletsPower = await PowerCmd.Apply<WallOfBulletsPower>(choiceContext, this.Owner.Creature, DynamicVars["BlockOnExhaust"].IntValue ,
            this.Owner.Creature, (CardModel)this);
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}