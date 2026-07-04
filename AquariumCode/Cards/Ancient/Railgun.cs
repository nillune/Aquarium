using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Godot;

using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
namespace Aquarium.AquariumCode.Cards.Ancient;

  

public class Railgun : AquariumCard
{
    public Railgun() : base(1, CardType.Attack, CardRarity.Ancient, TargetType.AllEnemies)
    {

       

    }
   
  //  protected override IEnumerable<string> ExtraRunAssetPaths => new[] { "vfx/vfx_hyperbeam" };
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new DamageVar(5, ValueProp.Move), new RepeatVar(5)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardCmdPatches.Weapon ];
    
    private async Task SpawnBeamsForAllTargets()
    {
        foreach (var enemy in CombatState.HittableEnemies)
        {
            var beam = NHyperbeamVfx.Create(Owner.Creature, enemy);
            if (beam != null)
            {
                NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(beam);
            }

            
            await Cmd.Wait(0.1f);
            return;
        }
    }
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {  
        await SpawnBeamsForAllTargets();
        await Cmd.Wait(.2f);
        
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this, play)
            .TargetingAllOpponents(CombatState)
            .WithHitCount(DynamicVars.Repeat.IntValue)
            .WithHitFx("vfx/vfx_starry_impact")
           // .WithAttackerAnim("Attack")
            .Execute(choiceContext);
            
    }
   
    protected override void OnUpgrade()
    {
        DynamicVars.Repeat.UpgradeValueBy(1m);
       // DynamicVars.Damage.UpgradeValueBy(1m);
    }
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    
    //Smaller variants of card images for efficiency:
    //Smaller variant of fullart: 250x350
    //Smaller variant of normalart: 250x190
    
    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}