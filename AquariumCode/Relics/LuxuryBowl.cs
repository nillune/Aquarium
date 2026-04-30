using Aquarium.AquariumCode.Character;
using Aquarium.AquariumCode.Extensions;
using Aquarium.AquariumCode.Relics;
using BaseLib.Abstracts;
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
public class LuxuryBowl() : AquariumRelic
{
   
    public override RelicRarity Rarity =>
        RelicRarity.Starter;
    public override async Task BeforeFlushLate(PlayerChoiceContext choiceContext, Player player)
    {
        LuxuryBowl source = this;
        LuxuryBowl luxuryBowl = this;
        if (player != source.Owner || !Hook.ShouldFlush(player.Creature.CombatState, player))
            return;
        CardSelectorPrefs prefs = new CardSelectorPrefs(source.SelectionScreenPrompt, 0, 2);
        List<CardModel> list = (await CardSelectCmd.FromHand(choiceContext, source.Owner, prefs, new Func<CardModel, bool>(RetainFilter), (AbstractModel) source)).ToList<CardModel>();
        if (list.Count == 0)
            return;
        foreach (CardModel cardModel in list)
            cardModel.GiveSingleTurnRetain();
        luxuryBowl.Flash();
    }

   
    private bool RetainFilter(CardModel card) => !card.ShouldRetainThisTurn;
}