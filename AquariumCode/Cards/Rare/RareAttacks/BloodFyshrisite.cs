using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Rare;

 
public class BloodFyshrisite() : AquariumCard(5,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {    
            this.EnergyHoverTip };
    }
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new DamageVar(13, ValueProp.Move), new EnergyVar(1)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardCmdPatches.Weapon,CardKeyword.Exhaust ];
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
      
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        AttackCommand attackCommand = await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).FromCard((CardModel) this).Targeting(
            play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        CardModel clone = this.CreateClone();
        clone.EnergyCost.SetThisCombat(this.EnergyCost.GetResolved()-DynamicVars.Energy.IntValue);
        CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(clone, PileType.Draw, this.Owner), 1.5f);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
    }
}