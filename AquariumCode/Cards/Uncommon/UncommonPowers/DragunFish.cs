using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class DragunFish() : AquariumCard(2,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromPower<VigorPower>()};
    }
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("Power",3), new PowerVar<VigorPower>(13)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardCmdPatches.Weapon ];
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<VigorPower>(
            choiceContext,   Owner.Creature,
            DynamicVars.Power<VigorPower>().IntValue,
            Owner.Creature,
            (CardModel) this);
        DragunFish dragunFish = this;
        DragunFish cardSource = this;
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        DragunFishPower dragunFishPower = await PowerCmd.Apply<DragunFishPower>(choiceContext, cardSource.Owner.Creature, cardSource.DynamicVars["Power"].BaseValue ,
            cardSource.Owner.Creature, (CardModel)cardSource);
    }

    protected override void OnUpgrade() => DynamicVars.Power<VigorPower>().UpgradeValueBy(4M);
}