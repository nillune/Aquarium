using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Powers;


public class BootstrapPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;


    private decimal RealVigor;



    public override (PileType, CardPilePosition) ModifyCardPlayResultPileTypeAndPosition(
        CardModel card,
        bool isAutoPlay,
        ResourceInfo resources,
        PileType pileType,
        CardPilePosition position)
    {
        BootstrapPower power = this;
        if (card.Owner.Creature != this.Owner)
            return (pileType, position);
        RealVigor = power.Owner.GetPowerAmount<VigorPower>();
        if (card.Type != CardType.Attack)
        {
            RealVigor = 0;
        }
        return  (pileType, position);
    }
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        BootstrapPower power = this;
        
       

        await PowerCmd.Apply<VigorPower>(
                power.Owner,
                RealVigor,
                power.Owner,
                (CardModel)null);
        
        // RealVigor = power.Owner.GetPowerAmount<VigorPower>();
        
    }

}