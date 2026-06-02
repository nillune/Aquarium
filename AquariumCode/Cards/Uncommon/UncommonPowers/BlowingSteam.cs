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

  
public class BlowingSteam() : AquariumCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("Power",2),new PowerVar<VigorPower>(5)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromPower<VigorPower>()};
    }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        BlowingSteamPower blowingSteamPower = await PowerCmd.Apply<BlowingSteamPower>(choiceContext, this.Owner.Creature, this.DynamicVars["Power"].BaseValue ,
            this.Owner.Creature, (CardModel)this);
        PermVigorNextTurnPower permVigorNextTurnPower = await PowerCmd.Apply<PermVigorNextTurnPower>(choiceContext, this.Owner.Creature,  DynamicVars[nameof(VigorPower)].BaseValue ,
            this.Owner.Creature, (CardModel)this);
    }

    protected override void OnUpgrade() =>  DynamicVars["VigorPower"].UpgradeValueBy(2m);
}