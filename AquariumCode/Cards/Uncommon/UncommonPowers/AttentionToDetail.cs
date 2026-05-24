using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Cards.Uncommon;

 
public class AttentionToDetail() : AquariumCard(2,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar (1) ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        AttentionToDetail attentionToDetail = this;
        AttentionToDetail cardSource = this;
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        AttentionToDetailPower attentionToDetailPower = await PowerCmd.Apply<AttentionToDetailPower>(cardSource.Owner.Creature, DynamicVars.Energy.IntValue ,
            cardSource.Owner.Creature, (CardModel)cardSource);
    }

    protected override void OnUpgrade() => this.DynamicVars.Energy.UpgradeValueBy(1M);
}