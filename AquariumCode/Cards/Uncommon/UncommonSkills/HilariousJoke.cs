using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Cards.Rare;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class HilariousJoke() : AquariumCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AllAllies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<VigorPower>(10)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromPower<VigorPower>()};
    }
    //protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Strike };
    public override CardMultiplayerConstraint MultiplayerConstraint  => CardMultiplayerConstraint.MultiplayerOnly;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        
      
        foreach (Creature creature in this.CombatState.GetTeammatesOf(this.Owner.Creature)
                     .Where<Creature>((Func<Creature, bool>)(c => c != null && c.IsAlive && c.IsPlayer)))
        {
            //MainFile.Logger.Info(this.Owner.Creature.Name + "     PLAYER NAME!!!!! LOOK AT THIS!!!     " + creature.Name);
            //hilariousJoke.Owner.Creature.Name
            if (this.Owner.Creature != creature)
            {
                await PowerCmd.Apply<VigorPower>(
                    choiceContext,  creature,
                    DynamicVars[nameof(VigorPower)].BaseValue,
                    Owner.Creature,
                    this);
            }
            //MainFile.Logger.Info("not self");
            
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["VigorPower"].UpgradeValueBy(3m);
    }
}