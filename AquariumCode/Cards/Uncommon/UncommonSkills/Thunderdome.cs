using Aquarium.AquariumCode.Cards;
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class Thunderdome() : AquariumCard(5,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<BufferPower>(1M)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {    
            HoverTipFactory.FromKeyword(CardKeyword.Exhaust) };
    }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ThunderdomePower thunderdomePower = await PowerCmd.Apply<ThunderdomePower>(choiceContext, this.Owner.Creature, this.DynamicVars["BufferPower"].BaseValue, this.Owner.Creature, (CardModel) this);
    
    }
    public override async Task AfterAutoPrePlayPhaseEnteredEarly(
        PlayerChoiceContext choiceContext,
        Player player)
    {
       CardCmd.RemoveKeyword(this, CardKeyword.Exhaust);
        CardPile pile = this.Pile;
        if ((pile != null ? (pile.Type != PileType.Exhaust ? 1 : 0) : 1) != 0 || player != this.Owner)
            return;
        await CardCmd.AutoPlay(choiceContext, (CardModel) this, (Creature) null);
    }

    protected override void OnUpgrade() => this.DynamicVars["BufferPower"].UpgradeValueBy(1M);
}