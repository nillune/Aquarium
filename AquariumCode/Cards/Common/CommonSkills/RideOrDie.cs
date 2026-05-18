using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aquarium.AquariumCode.Extensions;
using Aquarium.AquariumCode.Powers;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Common;

  
public class RideOrDie() : AquariumCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new BlockVar(16, ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        RideOrDie cardSource = this;
        RideOrDiePower rideOrDiePower = await PowerCmd.Apply<RideOrDiePower>(cardSource.Owner.Creature, 1M,
            cardSource.Owner.Creature, (CardModel)cardSource);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(4m);
    }
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    
    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190
    
    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}