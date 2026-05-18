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

namespace Aquarium.AquariumCode.Cards.Common;

  
public class StarterPistol() : AquariumCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{


    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromPower<VigorPower>()};
    }
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [ new DynamicVar(nameof(VigorNextTurnPower), 3M), new EnergyVar(2), ];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardCmdPatches.Weapon ];
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        StarterPistol cardSource = this;
        await CreatureCmd.TriggerAnim(cardSource.Owner.Creature, "Cast", cardSource.Owner.Character.CastAnimDelay);
        VigorNextTurnPower vigorNextTurnPower = await PowerCmd.Apply<VigorNextTurnPower>(cardSource.Owner.Creature, DynamicVars["VigorNextTurnPower"].BaseValue,
            cardSource.Owner.Creature, (CardModel)cardSource);
        EnergyNextTurnPower energyNextTurnPower = await PowerCmd.Apply<EnergyNextTurnPower>(cardSource.Owner.Creature,
            (Decimal)cardSource.DynamicVars.Energy.IntValue, cardSource.Owner.Creature, (CardModel)cardSource);
    }
    protected override void OnUpgrade()
    {
        this.DynamicVars.Energy.UpgradeValueBy(1M);
        DynamicVars["VigorNextTurnPower"].UpgradeValueBy(1M);
    }
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    
    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190
    
    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}

    

