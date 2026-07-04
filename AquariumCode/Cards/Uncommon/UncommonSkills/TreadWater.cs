using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class TreadWater() : AquariumCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(4)];
    //public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardKeyword.Ethereal ];
    //protected override IEnumerable<IHoverTip> ExtraHoverTips
  //  {
        
   // }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Decimal baseValue = this.DynamicVars.Cards.IntValue;
        int count = this.Owner.PlayerCombatState.Hand.Cards.Count;
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, Math.Max(0M, baseValue - (Decimal) count), this.Owner);
    }

    protected override void OnUpgrade() => this.DynamicVars.Cards.UpgradeValueBy(1M);
}