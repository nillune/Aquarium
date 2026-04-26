using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

#nullable enable
namespace MegaCrit.Sts2.Core.Models.Powers;

  
public sealed class VulnNextTurnPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        VulnNextTurnPower power = this;
        VulnNextTurnPower vulnNextTurnPower = this;
        if (player != vulnNextTurnPower.Owner.Player || vulnNextTurnPower.AmountOnTurnStart == 0)
            return;
        VulnerablePower vulnerablePower = await PowerCmd.Apply<VulnerablePower>(
            power.Owner,
            power.Amount,
            power.Owner,
            (CardModel) null);
        await PowerCmd.Remove((PowerModel)power);
    }
}