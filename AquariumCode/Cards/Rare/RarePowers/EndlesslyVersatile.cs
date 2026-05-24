using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Cards.Rare;

  
  
  
  
public class EndlesslyVersatile() : AquariumCard(2,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        foreach (CardModel allCard in this.Owner.PlayerCombatState.AllCards)
        {
           
                CardCmd.ApplyKeyword(allCard,  CardCmdPatches.Weapon );
        }

        return;
    }

    protected override void OnUpgrade() => this.AddKeyword(CardKeyword.Innate);
}