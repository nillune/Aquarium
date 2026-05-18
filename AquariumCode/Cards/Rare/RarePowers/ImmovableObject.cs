using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Rare;

  
public class ImmovableObject() : AquariumCard(3,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new BlockVar(10, ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ImmovableObjectPower immovableObjectPower = await PowerCmd.Apply<ImmovableObjectPower>(this.Owner.Creature, 1 ,
            this.Owner.Creature, (CardModel)this);

        if (this.IsUpgraded)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        }
    }

    protected override void OnUpgrade()
    {

    }
}