using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Powers;

 
public abstract class FinPolishPower : PowerModel
{
public override PowerType Type => PowerType.Buff;

public override PowerStackType StackType => PowerStackType.Counter;

public override async Task BeforeCardPlayed(CardPlay cardPlay)
                             {
    
    if (cardPlay.Card.Owner != this.Owner.Player || cardPlay.Card.Type != CardType.Skill)
        return;
    VigorPower vigorPower = await PowerCmd.Apply<VigorPower>(
        this.Owner,
        this.Amount,
        this.Owner,
        (CardModel) null);
}
public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
{
    FinPolishPower power = this;
    await PowerCmd.Remove((PowerModel) power);
}
}