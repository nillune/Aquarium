using Aquarium.AquariumCode.Cards.Rare;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Powers;

public class CauldronOfEverythingPower: CustomTemporaryPowerModelWrapper<CauldronOfEverythingPower, StrengthPower>
{
   
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
    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
       
        if (side != this.Owner.Side)
            return;
        this.Flash();
        await PowerCmd.Remove((PowerModel) this);
        StrengthPower strengthPower = await PowerCmd.Apply<StrengthPower>(this.Owner, (Decimal) (-1 * this.Amount), this.Owner, (CardModel) null);
    }
    public override AbstractModel OriginModel => (AbstractModel) ModelDb.Card<CauldronOfEverything>();

   
}