using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Cards.Rare;

  
public class BlowingSteam() : AquariumCard(1,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("Power",3)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        BlowingSteamPower blowingSteamPower = await PowerCmd.Apply<BlowingSteamPower>(this.Owner.Creature, this.DynamicVars["Power"].BaseValue ,
            this.Owner.Creature, (CardModel)this);
    }

    protected override void OnUpgrade() => this.DynamicVars["Power"].UpgradeValueBy(1M);
}