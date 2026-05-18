
using Aquarium.AquariumCode.Extensions;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;

using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class AmmoBag() : AquariumCard(3,    
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar (2) ];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardCmdPatches.Weapon ];
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        AmmoBag ammoBag = this;
        await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, ammoBag.Owner );
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Energy.UpgradeValueBy(1M);
    }
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    
    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190
    
    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}