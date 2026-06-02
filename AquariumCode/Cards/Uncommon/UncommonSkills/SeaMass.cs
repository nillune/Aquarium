using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class SeaMass() : AquariumCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new BlockVar(10, ValueProp.Move), new RepeatVar(2)];
    protected override bool ShouldGlowGoldInternal => this.Owner.Creature.HasPower<FrailPower>();
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromPower<FrailPower>()};
    }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int blockCount = this.Owner.Creature.HasPower<FrailPower>() ? this.DynamicVars.Repeat.IntValue : 1; 
        
       
            for (int i = 1; i <= blockCount ; ++i)
            {
                await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
            }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Repeat.UpgradeValueBy(1m);
    }
}