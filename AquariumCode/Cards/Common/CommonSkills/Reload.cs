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
using MegaCrit.Sts2.Core.Models.Cards;

namespace Aquarium.AquariumCode.Cards.Common;

  
public class Reload() : AquariumCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar(nameof(Blur), 1M)];
  
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardCmdPatches.Weapon, ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {  HoverTipFactory.Static(StaticHoverTip.Block), HoverTipFactory.FromPower<VigorPower>()};
    }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Reload cardSource = this;
        DisassemblyPower disassemblyPower = await PowerCmd.Apply<DisassemblyPower>(cardSource.Owner.Creature, 1M,
            cardSource.Owner.Creature, (CardModel)cardSource);
        BlurPower blurPower = await PowerCmd.Apply<BlurPower>(cardSource.Owner.Creature, cardSource.DynamicVars[nameof(Blur)].BaseValue, cardSource.Owner.Creature, (CardModel) cardSource);
    }

    protected override void OnUpgrade()
    {
   this.EnergyCost.UpgradeBy(-1);
       // DynamicVars["Blur"].UpgradeValueBy(1M);
    }
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    
    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190
    
    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}