// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Powers.ViciousPower
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 623673A3-2F6A-4E15-A560-4F44F2297867
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll

using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable
namespace MegaCrit.Sts2.Core.Models.Powers;

 
public sealed class AintBrokePower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

 

    public override async Task AfterPowerAmountChanged(
        PowerModel power,
        Decimal amount,
        Creature? target,
        CardModel? cardSource)
    {
        AintBrokePower aintBrokePower = this;
        if (amount <= 0M || target != aintBrokePower.Owner || !(power is FrailPower))
            return;
        aintBrokePower.Flash();
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw((PlayerChoiceContext) new BlockingPlayerChoiceContext(), (Decimal) aintBrokePower.Amount, aintBrokePower.Owner.Player);
    }
}