using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Powers;


public class PortableBubblerPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    public override async Task BeforeHandDraw(
        Player player,
        PlayerChoiceContext choiceContext,
        CombatState combatState)
    {
        PortableBubblerPower power = this;
        PortableBubblerPower portableBubblerPower = this;
        if (power.Owner.Player != player)
            return;
        VigorPower vigorPower = await PowerCmd.Apply<VigorPower>(
            power.Owner,
            power.Amount,
            power.Owner,
            (CardModel) null);
        power.Flash();
        await CreatureCmd.GainBlock(portableBubblerPower.Owner, (Decimal)portableBubblerPower.Amount, ValueProp.Unpowered, (CardPlay)null);
       
    }
    
}