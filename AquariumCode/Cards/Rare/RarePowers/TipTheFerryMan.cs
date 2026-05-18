using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Cards.Rare;

 
public class TipTheFerryMan() : AquariumCard(3,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        TipTheFerrymanPower tipTheFerrymanPower = await PowerCmd.Apply<TipTheFerrymanPower>(this.Owner.Creature, 1 ,
            this.Owner.Creature, (CardModel)this);
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}