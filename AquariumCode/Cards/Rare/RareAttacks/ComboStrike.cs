using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Rare;

  
public class ComboStrike() : AquariumCard(1,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new DamageVar(11, ValueProp.Move), new CardsVar(1), new EnergyVar (1)];
    protected override bool ShouldGlowGoldInternal
    {
        get
        {
            return PileType.Hand.GetPile(this.Owner).Cards.Count<CardModel>((Func<CardModel, bool>) (c => c.Type == CardType.Attack)) == 1;
        }
    }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        if (PileType.Hand.GetPile(this.Owner).Cards.Any<CardModel>((Func<CardModel, bool>) (c => c.Type == CardType.Attack)))
            return;
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, this.DynamicVars.Cards.BaseValue, this.Owner);
        await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, this.Owner );
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(4m);
    }
}