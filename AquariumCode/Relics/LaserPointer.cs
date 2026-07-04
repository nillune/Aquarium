using Aquarium.AquariumCode.Character;
using Aquarium.AquariumCode.Relics;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;

namespace Aquarium.AquariumCode.Relics;

[Pool(typeof(AquariumRelicPool))]
public class LaserPointer() : AquariumRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Uncommon;
    public override Task BeforeCombatStart()
    {
        
       
        this.Status = RelicStatus.Active;
        return Task.CompletedTask;
    }
    public override Task AfterCombatEnd(CombatRoom room)
    {
        
        this.Status = RelicStatus.Normal;
        return Task.CompletedTask;
    }
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (this.Status == RelicStatus.Disabled)
            return;
        if (cardPlay.Card.Owner != this.Owner)
            return;
        if (cardPlay.Card.Type == CardType.Power)
        {
            CardModel card = (await CardSelectCmd.FromSimpleGrid(context,
                (IReadOnlyList<CardModel>)PileType.Draw.GetPile(this.Owner).Cards
                    .OrderBy<CardModel, CardRarity>((Func<CardModel, CardRarity>)(c => c.Rarity))
                    .ThenBy<CardModel, ModelId>((Func<CardModel, ModelId>)(c => c.Id)).ToList<CardModel>(), this.Owner,
                new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1))).FirstOrDefault<CardModel>();
            if (card == null)
                return;
            await CardCmd.Exhaust(context, card);
            this.Status = RelicStatus.Disabled;
        }

    }
}