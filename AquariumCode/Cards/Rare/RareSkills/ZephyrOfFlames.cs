using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Rare;


public class ZephyrOfFlames() : AquariumCard(2,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardKeyword.Exhaust ];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<VigorPower>(7)];
  //  protected override IEnumerable<IHoverTip> ExtraHoverTips
   // {
       // get => new[] {     HoverTipFactory.FromPower<ArtifactPower>(), HoverTipFactory.Static(StaticHoverTip.Block)
            //, HoverTipFactory.FromPower<VulnerablePower>()
        //  };
   // }
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        foreach (Creature creature in this.CombatState.GetTeammatesOf(this.Owner.Creature)
                     .Where<Creature>((Func<Creature, bool>)(c => c != null && c.IsAlive && c.IsPlayer)))
        {

            VigorPower vigorPower = await PowerCmd.Apply<VigorPower>(choiceContext, creature,
                (Decimal)DynamicVars[nameof(VigorPower)].IntValue, this.Owner.Creature, (CardModel)this);
        }
        foreach (Creature enemy in CombatState.HittableEnemies)
        {
            
            VigorPower vigorPower = await PowerCmd.Apply<VigorPower>(choiceContext, enemy,
                (Decimal)DynamicVars[nameof(VigorPower)].IntValue, this.Owner.Creature, (CardModel)this);
        }
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}