using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Extensions;
using Aquarium.AquariumCode.Powers;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands; // Or wherever AnchovyPower is located
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Common;

  
public class Bearing() : AquariumCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new DamageVar(0, ValueProp.Move)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardCmdPatches.Weapon ];
   
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int KeepVigor;
     KeepVigor = Owner.Creature.GetPowerAmount<VigorPower>();
     await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
         .FromCard(this)
         .Targeting(play.Target)
         .WithHitCount(DynamicVars.Repeat.IntValue)
         .WithHitFx("vfx/vfx_attack_slash")
         .Execute(choiceContext);
     await PowerCmd.Apply<VigorPower>(
         Owner.Creature,
         KeepVigor,
         Owner.Creature,
         this);
    }

    

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    
    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190
    
    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}