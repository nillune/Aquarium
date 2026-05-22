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

  
public class PartTheSea() : AquariumCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    private const string _strengthLossKey = "StrengthLoss";
    protected override IEnumerable<DynamicVar> CanonicalVars => [  new DynamicVar("StrengthLoss", 10M),
        new DynamicVar("StrengthGain", 6M)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => HoverTipFactory.FromPowerWithPowerHoverTips<StrengthPower>();
    }
    

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "play.Target");
        PartTheSeaPowerLoss  partTheSeaPowerLoss = await PowerCmd.Apply<PartTheSeaPowerLoss>(play.Target, this.DynamicVars["StrengthLoss"].BaseValue*-1, this.Owner.Creature, (CardModel) this);

        foreach (Creature hittableEnemy in (IEnumerable<Creature>) this.CombatState.HittableEnemies)
        {

            if (hittableEnemy != play.Target)
            {
                PartTheSeaPower partTheSeaPower = await PowerCmd.Apply<PartTheSeaPower>(hittableEnemy,
                    this.DynamicVars["StrengthGain"].BaseValue, this.Owner.Creature, (CardModel)this);
            }

        }
        
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars["StrengthLoss"].UpgradeValueBy(5M);
        this.DynamicVars["StrengthGain"].UpgradeValueBy(-1M);
    }
}