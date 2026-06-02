using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  //rip old feel pain
public class FeelPain() : AquariumCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new BlockVar(3, ValueProp.Move),];
   
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromKeyword(CardKeyword.Exhaust)};
    }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        FeelPain feelPain = this;
      
        CardPile pile = PileType.Hand.GetPile(this.Owner);
        for (int i = 0; i < 3; i++)
        {
            CardModel card2 =
                this.Owner.RunState.Rng.CombatCardSelection.NextItem<CardModel>((IEnumerable<CardModel>)pile.Cards);
            if (card2 == null)
                return;
            await CardCmd.Exhaust(choiceContext, card2);
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(1m);
      
    }
}