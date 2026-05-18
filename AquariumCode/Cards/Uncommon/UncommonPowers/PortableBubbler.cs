using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class PortableBubbler() : AquariumCard(2,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromPower<VigorPower>()};
    }
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("Power",3)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
      
        PortableBubbler cardSource = this;
      
        PortableBubblerPower portableBubblerPower = await PowerCmd.Apply<PortableBubblerPower>(cardSource.Owner.Creature, cardSource.DynamicVars["Power"].BaseValue ,
            cardSource.Owner.Creature, (CardModel)cardSource);
    }

    protected override void OnUpgrade() => this.DynamicVars["Power"].UpgradeValueBy(2M);
}