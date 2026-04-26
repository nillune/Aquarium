using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class Pour() : AquariumCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
   
    protected override bool HasEnergyCostX => true;
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new BlockVar(7, ValueProp.Move)];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Pour card = this;
        Pour source = this;
        int xValue = source.ResolveEnergyXValue();
        for (int i = 0; i < xValue; ++i)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
        }
        // ArgumentNullException.ThrowIfNull((object) cardPlay.Target, "cardPlay.Target");
       
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(2m);
    }
}