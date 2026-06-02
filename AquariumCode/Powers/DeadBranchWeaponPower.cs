using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Aquarium.AquariumCode.Powers;

  
public class DeadBranchWeaponPower: CustomPowerModel
{

   


    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterCardExhausted(
        PlayerChoiceContext choiceContext,
        CardModel card,
        bool _)
    {
        if (card.Keywords.Contains(CardCmdPatches.Weapon))
        {
            foreach (CardModel card2 in CardFactory.GetForCombat(this.Owner.Player,
                this.Owner.Player.Character.CardPool
                    .GetUnlockedCards(this.Owner.Player.UnlockState, this.Owner.Player.RunState.CardMultiplayerConstraint)
                    .Where<CardModel>((Func<CardModel, bool>)(c =>
                    {
                        return c.Keywords.Contains(CardCmdPatches.Weapon);
                    })), 1, this.Owner.Player.RunState.Rng.CombatCardGeneration))
            {
              
                CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(card2, PileType.Hand, this.Owner.Player);
            }
        }
    }
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
}