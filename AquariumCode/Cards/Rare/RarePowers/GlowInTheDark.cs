using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Rare;

  
public class GlowInTheDark() : AquariumCard(1,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromPower<StrengthPower>()};
    }
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new DynamicVar("EnemyStrength", 2M), new EnergyVar (1)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        GlowInTheDarkPower glowInTheDarkPower = await PowerCmd.Apply<GlowInTheDarkPower>(choiceContext, this.Owner.Creature, DynamicVars.Energy.IntValue,
            this.Owner.Creature, (CardModel)this);
        foreach (Creature hittableEnemy in (IEnumerable<Creature>)this.CombatState.HittableEnemies)
        {

            StrengthPower strengthPower2 = await PowerCmd.Apply<StrengthPower>(choiceContext, hittableEnemy,
                this.DynamicVars["EnemyStrength"].BaseValue, this.Owner.Creature, (CardModel)this);
            /*
            DemonFormPower demonFormPower = await PowerCmd.Apply<DemonFormPower>(hittableEnemy,
                this.DynamicVars["EnemyStrength"].BaseValue, this.Owner.Creature, (CardModel)this);
            SerpentFormPower serpentFormPower = await PowerCmd.Apply<SerpentFormPower>(hittableEnemy,
                this.DynamicVars["EnemyStrength"].BaseValue, this.Owner.Creature, (CardModel)this);
            VoidFormPower voidFormPower = await PowerCmd.Apply<VoidFormPower>(hittableEnemy,
                this.DynamicVars["EnemyStrength"].BaseValue, this.Owner.Creature, (CardModel)this);
            ReaperFormPower reaperFormPower = await PowerCmd.Apply<ReaperFormPower>(hittableEnemy,
                this.DynamicVars["EnemyStrength"].BaseValue, this.Owner.Creature, (CardModel)this);
            EchoFormPower echoFormPower = await PowerCmd.Apply<EchoFormPower>(hittableEnemy,
                this.DynamicVars["EnemyStrength"].BaseValue, this.Owner.Creature, (CardModel)this);
           */
        }
    }
 


    protected override void OnUpgrade() => this.AddKeyword(CardKeyword.Innate);
}