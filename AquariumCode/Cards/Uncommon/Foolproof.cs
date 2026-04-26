using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class Foolproof() : AquariumCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Foolproof foolproof = this;
        Foolproof cardSource = this;
      
        FoolproofPower pillarOfJunkPower = await PowerCmd.Apply<FoolproofPower>(cardSource.Owner.Creature, 1 ,
            cardSource.Owner.Creature, (CardModel)cardSource);
    }

    
    protected override void OnUpgrade() => this.AddKeyword(CardKeyword.Innate);
    
}