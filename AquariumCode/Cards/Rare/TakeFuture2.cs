using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Rare;

  
  
public class TakeFuture2() : AquariumCard(0,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar (2), new PowerVar<VigorPower>(10), new CardsVar(3), new PowerVar<FrailPower>(99),new PowerVar<VulnerablePower>(99)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardKeyword.Exhaust ];
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        TakeFuture2 takeFuture = this;
        await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, takeFuture.Owner );
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.IntValue, takeFuture.Owner);
        TakeFuture2 cardSource = this;
        VulnNextTurnPower vulnNextTurnPower = await PowerCmd.Apply<VulnNextTurnPower>(cardSource.Owner.Creature, 99M,
            cardSource.Owner.Creature, (CardModel)cardSource);
        await PowerCmd.Apply<VigorPower>(
            Owner.Creature,
            DynamicVars[nameof(VigorPower)].BaseValue,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Energy.UpgradeValueBy(1M);
    }
}