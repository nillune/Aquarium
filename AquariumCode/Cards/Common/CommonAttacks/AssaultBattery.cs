using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Extensions;
using BaseLib.Patches.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Common;

  
public class AssaultBattery() : AquariumCard(2,
    CardType.Attack, CardRarity.Common,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new DamageVar(10, ValueProp.Move),  (DynamicVar) new PowerVar<VulnerablePower>(2M) ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        AssaultBattery assaultBattery = this;
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .TargetingAllOpponents(CombatState)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);

        foreach (Creature enemy in CombatState.HittableEnemies)
        {
            VulnerablePower vulnerablePower = await PowerCmd.Apply<VulnerablePower>(enemy,
                assaultBattery.DynamicVars.Vulnerable.BaseValue, assaultBattery.Owner.Creature, (CardModel)assaultBattery);
        }
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(5M);
    }
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    
    //Smaller variants of card images for efficiency:
    //Smaller variant of full art: 250x350
    //Smaller variant of normal art: 250x190
    
    //Uses card_portraits/card_name.png as image path. These should be smaller images.
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}