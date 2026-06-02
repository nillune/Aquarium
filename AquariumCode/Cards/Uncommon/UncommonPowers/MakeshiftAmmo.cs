
using Aquarium.AquariumCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;


namespace Aquarium.AquariumCode.Cards.Uncommon;

 
public class MakeshiftAmmo() : AquariumCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ("Power",3)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromPower<VigorPower>(), HoverTipFactory.FromKeyword(CardKeyword.Exhaust)};
    }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        MakeshiftAmmoPower makeshiftAmmoPower = await PowerCmd.Apply<MakeshiftAmmoPower>(choiceContext, this.Owner.Creature, this.DynamicVars["Power"].BaseValue ,
            this.Owner.Creature,this);
    }

    protected override void OnUpgrade()
    {
 AddKeyword(CardKeyword.Innate);
    }
}