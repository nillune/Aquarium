using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Cards.Uncommon;

 
public class Octorifle() : AquariumCard(1,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new DamageVar(9, ValueProp.Move)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardCmdPatches.Weapon ];
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Octorifle octorifle = this;
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
        CardSelectorPrefs prefs = new CardSelectorPrefs(octorifle.SelectionScreenPrompt, 1);
        CardModel card = (await CardSelectCmd.FromHand(choiceContext, octorifle.Owner, prefs, (Func<CardModel, bool>) (c => c.Type == CardType.Attack), (AbstractModel) octorifle)).FirstOrDefault<CardModel>();
        if (card == null)
            return;
       
        CardCmd.Enchant<Inky>(card, 1M);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
    }
}