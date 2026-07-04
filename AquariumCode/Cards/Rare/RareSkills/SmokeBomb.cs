using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Rare;

 
public class SmokeBomb() : AquariumCard(2,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("Turns",2), new DynamicVar("BombDexterity",5)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromPower<DexterityPower>()};
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        SmokeBomb cardSource = this;
        (await PowerCmd.Apply<SmokeBombPower>(choiceContext, cardSource.Owner.Creature, cardSource.DynamicVars["Turns"].BaseValue, cardSource.Owner.Creature, (CardModel) cardSource)).SetDexterity(cardSource.DynamicVars["BombDexterity"].BaseValue);
    }

    
    protected override void OnUpgrade() => this.DynamicVars["BombDexterity"].UpgradeValueBy(2M);
    
}