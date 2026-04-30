// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Powers.EnergyNextTurnPower
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 623673A3-2F6A-4E15-A560-4F44F2297867
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll

using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;

#nullable enable
namespace MegaCrit.Sts2.Core.Models.Powers;

  
public sealed class VigorBlock : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

   

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        VigorBlock power = this;
         if (side != CombatSide.Player)
              return;
        VigorPower vigorPower = await PowerCmd.Apply<VigorPower>(
            power.Owner,
            power.Owner.Block,
            power.Owner,
            (CardModel) null);
        power.Flash();
        await PowerCmd.Remove((PowerModel) power);
    }
}