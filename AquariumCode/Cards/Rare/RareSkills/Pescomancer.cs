using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Rare;

  
public class Pescomancer() : AquariumCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    private const string _increaseKey = "Increase";
   
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(1M, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move),
        new RepeatVar(7),  new DynamicVar("Increase", 1M)];
    private Decimal _extraDamage;
    private Decimal ExtraDamage
    {
        get => this._extraDamage;
        set
        {
            this.AssertMutable();
            this._extraDamage = value;
        }
    }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Pescomancer pescomancer = this;
        Pescomancer cardSource = this;
        for (int i = 0; i < DynamicVars.Repeat.IntValue; i++)
        {
            IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage(choiceContext, play.Target,
                cardSource.DynamicVars.Damage, (CardModel)cardSource);
        }

        Decimal baseValue = this.DynamicVars["Increase"].BaseValue;
        DamageVar damage = this.DynamicVars.Damage;
        damage.BaseValue = damage.BaseValue + baseValue;
        this.ExtraDamage += baseValue;
        CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat((CardModel) pescomancer.CombatState.CreateCard<Dazed>(pescomancer.Owner), PileType.Draw, true));
    }
    
    protected override void OnUpgrade()
    {
        DynamicVars.Repeat.UpgradeValueBy(1m);
    }
}