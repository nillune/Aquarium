using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Powers;

 
public class FingerGunsPower :  CustomPowerModel
{

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


public override PowerType Type => PowerType.Buff;

public override PowerStackType StackType => PowerStackType.Single;

public override async Task AfterCardExhausted(
    PlayerChoiceContext choiceContext,
    CardModel card,
    bool _)
{
    if (card.Keywords.Contains(CardCmdPatches.Weapon))
    {
        foreach (Creature creature in this.CombatState.GetTeammatesOf(this.Owner)
                     .Where<Creature>((Func<Creature, bool>)(c => c != null && c.IsAlive && c.IsPlayer)))
        {
            //MainFile.Logger.Info(this.Owner.Creature.Name + "     PLAYER NAME!!!!! LOOK AT THIS!!!     " + creature.Name);
            //hilariousJoke.Owner.Creature.Name
            if (this.Owner != creature)
            {
                card.SetToFreeThisTurn();
                CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, creature.Player);
            }
        }

      
    }
}
}