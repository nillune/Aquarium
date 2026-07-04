using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Rare;

 
public class Carried() : AquariumCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.AnyPlayer)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new PowerVar<FrailPower>(2),
        new PowerVar<DexterityPower>(2)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromPower<DexterityPower>(), HoverTipFactory.FromPower<FrailPower>()};
    }
    //protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Strike };
    public override CardMultiplayerConstraint MultiplayerConstraint  => CardMultiplayerConstraint.MultiplayerOnly;
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "play.Target");
        await PowerCmd.Apply<FrailPower>(
            choiceContext, 
            Owner.Creature,
            DynamicVars[nameof(FrailPower)].BaseValue,
            Owner.Creature,
            this);
        
           
                await PowerCmd.Apply<DexterityPower>(
                    choiceContext, play.Target.Player.Creature,
                    DynamicVars[nameof(DexterityPower)].BaseValue,
                    Owner.Creature,
                    this);
            
        

        //await PlayerCmd.GainEnergy((Decimal) carried.DynamicVars.Energy.IntValue, creature.Player);
      
    }   

    protected override void OnUpgrade()
    {
        DynamicVars["DexterityPower"].UpgradeValueBy(1m);
    }
}