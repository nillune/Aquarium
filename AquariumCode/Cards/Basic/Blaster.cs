
using Aquarium.AquariumCode.Extensions;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Basic;

  
public class Blaster : AquariumCard
{
    public Blaster() : base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
    {

       

    }
        
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new DamageVar(2, ValueProp.Move), new RepeatVar(4)];
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {  
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitCount(DynamicVars.Repeat.IntValue)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }
   
    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(1m);
         this.AddKeyword(CardCmdPatches.Weapon);
    }
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    
    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190
    
    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}