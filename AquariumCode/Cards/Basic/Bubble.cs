using Aquarium.AquariumCode.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;

using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;

namespace Aquarium.AquariumCode.Cards.Basic;


public class Bubble : AquariumCard

{
    public Bubble() : base(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        
       
    }
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromPower<DoomPower>()};
    }
    protected override IEnumerable<DynamicVar> CanonicalVars => [  new BlockVar(3, ValueProp.Move), new PowerVar<VigorPower>(3)];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await PowerCmd.Apply<VigorPower>(
            Owner.Creature,
            DynamicVars[nameof(VigorPower)].BaseValue,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
    //Image size:
    //Normal art: 1000x760 (Using 500x380 should also work, it will simply be scaled.)
    //Full art: 606x852
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    
    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190
    
    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}