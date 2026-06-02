using Aquarium.AquariumCode.Cards.Rare;
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


namespace Aquarium.AquariumCode.Powers;

#nullable enable


public class PartTheSeaPowerLoss :  CustomTemporaryPowerModelWrapper<PartTheSeaPowerLoss, StrengthPower>
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
   

    public override AbstractModel OriginModel => (AbstractModel) ModelDb.Card<PartTheSea>();
    public override async Task AfterSideTurnEnd(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IEnumerable<Creature> participants)
    {
       
        if (side != this.Owner.Side)
            return;
        this.Flash();
        await PowerCmd.Remove((PowerModel) this);
        StrengthPower strengthPower = await PowerCmd.Apply<StrengthPower>(new ThrowingPlayerChoiceContext(),this.Owner, (Decimal) (-1 * this.Amount), this.Owner, (CardModel) null);
    }


}