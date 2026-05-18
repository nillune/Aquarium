using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Cards.Uncommon;

 
public class Sphereheaded() : AquariumCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Sphereheaded sphereheaded = this;
        
        await OrbCmd.Channel(choiceContext, OrbModel.GetRandomOrb(sphereheaded.Owner.RunState.Rng.CombatOrbGeneration).ToMutable(), sphereheaded.Owner);
        if (sphereheaded.IsUpgraded)
        {
            await Cmd.CustomScaledWait(0.1f, 0.25f);
            await OrbCmd.EvokeNext(choiceContext, sphereheaded.Owner,  false);
        }
        await OrbCmd.EvokeNext(choiceContext, sphereheaded.Owner);
      
       
       
    }

    protected override void OnUpgrade()
    {

    }
}