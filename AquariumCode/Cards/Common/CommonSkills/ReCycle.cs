using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Extensions;
using Aquarium.AquariumCode.Powers;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Common;

 
public class ReCycle() : AquariumCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new BlockVar(5, ValueProp.Move), new EnergyVar(1)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[]
        {
            HoverTipFactory.FromKeyword(CardKeyword.Exhaust)
        };
    }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        
            CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1);
            CardModel card2 = (await CardSelectCmd.FromHand(choiceContext, this.Owner, prefs, (Func<CardModel, bool>) null, (AbstractModel) this)).FirstOrDefault<CardModel>();
            if (card2 == null)
                return;
            await CardCmd.Exhaust(choiceContext, card2);
            if (card2.EnergyCost.GetResolved() > 0)
            {
                DynamicVars.Block.BaseValue = DynamicVars.Block.IntValue * card2.EnergyCost.GetResolved();
                await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block , play);
                //why the hell do i need to do it like this let me put a decimal in for amount of block hello
                DynamicVars.Block.BaseValue = 5;
                if (this.IsUpgraded)
                    DynamicVars.Block.BaseValue++;
            
            }
            /*
        ReCycle cardSource = this;
        ReCyclePower reCyclePower = await PowerCmd.Apply<ReCyclePower>(cardSource.Owner.Creature, 1M,
            cardSource.Owner.Creature, (CardModel)cardSource);
        foreach (CardModel card in PileType.Hand.GetPile(this.Owner).Cards)
        {

            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        }
        */
       
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(1m);
    }
    private  IEnumerable<CardModel> GetCards()
    {
        return PileType.Hand.GetPile(this.Owner).Cards;
    }
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    
    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190
    
    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}