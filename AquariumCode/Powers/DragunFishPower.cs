using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Powers;

 
public class DragunFishPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        DragunFishPower power = this;
        if (cardPlay.Card.Type != CardType.Attack && cardPlay.Card.TargetType != TargetType.AllEnemies)
            return;
        VigorPower vigorPower = await PowerCmd.Apply<VigorPower>(
            power.Owner,
            power.Owner.Block,
            power.Owner,
            (CardModel) null);
        power.Flash();
    
    }
}