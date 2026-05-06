// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Powers.DarkShacklesPower
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 623673A3-2F6A-4E15-A560-4F44F2297867
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll

using Aquarium.AquariumCode.Cards.Uncommon;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;

#nullable enable
namespace Aquarium.AquariumCode.Powers;

public class SodaMintBombPower : TemporaryStrengthPower
{
    public override AbstractModel OriginModel => (AbstractModel) ModelDb.Card<SodaMintBomb>();

    protected override bool IsPositive => false;
}