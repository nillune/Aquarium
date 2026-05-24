using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class EnthrallingAnger() : AquariumCard(2,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new DamageVar(20, ValueProp.Move), new BlockVar(10, ValueProp.Move)];
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {  
        
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
    }
    public override bool ShouldPlay(CardModel card, AutoPlayType autoPlayType)
    {
        if (card.Owner != this.Owner)
            return true;
        CardPile pile = this.Pile;
        
        return (pile != null ? (pile.Type != PileType.Hand ? 1 : 0) : 1) != 0 || card is EnthrallingAnger || autoPlayType != AutoPlayType.None;
        
    }

protected override void OnUpgrade()
{
    DynamicVars.Damage.UpgradeValueBy(4m);
    DynamicVars.Block.UpgradeValueBy(4m);
}
}
