// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Powers.TheBombPower
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 623673A3-2F6A-4E15-A560-4F44F2297867
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll

using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

#nullable enable

namespace Aquarium.AquariumCode.Powers;
public sealed class SmokeBombPower : CustomPowerModel
{

   


  public override PowerType Type => PowerType.Buff;

  public override PowerStackType StackType => PowerStackType.Counter;

  public override bool IsInstanced => true;

 
  protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<DexterityPower>(5)];

  public void SetDexterity(Decimal dexterity)
  {
    this.AssertMutable();
    DynamicVars["DexterityPower"].BaseValue = dexterity;
  }

  public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
  {
    SmokeBombPower power = this;
    if (side != power.Owner.Side)
      return;
    if (power.Amount > 1)
    {
      await PowerCmd.Decrement((PowerModel) power);
    }
    else
    {
      power.Flash();
      await PowerCmd.Apply<DexterityPower>(
        power.Owner,
        DynamicVars[nameof(DexterityPower)].BaseValue,
        power.Owner,
        (CardModel) null);
      
      await PowerCmd.Remove((PowerModel) power);
    }
  }
}
