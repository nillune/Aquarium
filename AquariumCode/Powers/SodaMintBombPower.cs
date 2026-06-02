// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Powers.DarkShacklesPower
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 623673A3-2F6A-4E15-A560-4F44F2297867
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll

using Aquarium.AquariumCode.Cards.Uncommon;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;


#nullable enable
namespace Aquarium.AquariumCode.Powers;

public class SodaMintBombPower : CustomTemporaryPowerModelWrapper<SodaMintBombPower, StrengthPower>
{
    //protected  bool InvertInternalPowerAmount => true;
    public override PowerType Type => PowerType.Debuff;
    public override string CustomPackedIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
            
            return ResourceLoader.Exists(path) ? path : "power.png".PowerImagePath();
        }
    }

    public override string CustomBigIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
           
            return ResourceLoader.Exists(path) ? path : "power.png".BigPowerImagePath();
        }
    }


    public override AbstractModel OriginModel => (AbstractModel) ModelDb.Card<SodaMintBomb>();

    public override async Task AfterSideTurnEnd(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IEnumerable<Creature> participants)
    {
       
        if (side != this.Owner.Side)
            return;
        this.Flash();
        await PowerCmd.Remove((PowerModel) this);
        StrengthPower strengthPower = await PowerCmd.Apply<StrengthPower>(new ThrowingPlayerChoiceContext(), this.Owner, (Decimal) (-1 * this.Amount), this.Owner, (CardModel) null);
    }
}

