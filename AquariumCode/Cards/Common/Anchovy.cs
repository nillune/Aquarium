using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Common;

  
public class Anchovy() : AquariumCard(0,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardKeyword.Exhaust ];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int KeepVigor;
        Anchovy anchovy = this;
        Anchovy cardSource = this;
        if (Owner.HasPower<VigorPower>())
        {
            
            
            VigorNextTurnPower vigorNextTurnPower = await PowerCmd.Apply<VigorNextTurnPower>(cardSource.Owner.Creature,Owner.Creature.GetPowerAmount<VigorPower>() ,
                cardSource.Owner.Creature, (CardModel)cardSource);
            VigorPower vigorPower = await PowerCmd.Apply<VigorPower>(
               Owner.Creature,
                -Owner.Creature.GetPowerAmount<VigorPower>(),
                Owner.Creature,
                (CardModel) null);
           
        }
        IEnumerable<CardModel> cardModels =
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.IntValue, anchovy.Owner);
    }

    protected override void OnUpgrade() => this.RemoveKeyword(CardKeyword.Exhaust);
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    
    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190
    
    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}