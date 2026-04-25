using Aquarium.AquariumCode.Character;
using Aquarium.AquariumCode.Extensions;
using Aquarium.AquariumCode.Relics;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Relics;

 
[Pool(typeof(AquariumRelicPool))]
public class DecroratedBowl() : AquariumRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;
    public override async Task BeforeFlushLate(PlayerChoiceContext choiceContext, Player player)
    {
        DecroratedBowl source = this;
        DecroratedBowl decroratedBowl = this;
        if (player != source.Owner || !Hook.ShouldFlush(player.Creature.CombatState, player))
            return;
        CardPile pile = PileType.Hand.GetPile(source.Owner);
        CardModel card = Owner.RunState.Rng.CombatCardSelection.NextItem<CardModel>((IEnumerable<CardModel>) pile.Cards);
        if (card == null)
            return;
            card.GiveSingleTurnRetain();
            decroratedBowl.Flash();
    }

   
  
}
