using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class FeelPain() : AquariumCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new BlockVar(11, ValueProp.Move), new PowerVar<StrengthPower>(1M),];
    protected override bool ShouldGlowGoldInternal => this.Owner.Creature.HasPower<FrailPower>();
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        FeelPain feelPain = this;
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        if (feelPain.Owner.Creature.HasPower<FrailPower>()) {
            await PowerCmd.Apply<StrengthPower>(
                Owner.Creature,
                DynamicVars[nameof(StrengthPower)].BaseValue,
                Owner.Creature,
                this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(4m);
    }
}