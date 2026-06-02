using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace Aquarium.AquariumCode.Cards.Uncommon;


public class KnifeGun() : AquariumCard(3,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardCmdPatches.Weapon];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(5)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {    
           HoverTipFactory.FromCard<Shiv>() };
    }
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {

        KnifeGun knifeGun = this;
        IEnumerable<CardModel> list =
            (IEnumerable<CardModel>)PileType.Hand.GetPile(knifeGun.Owner).Cards.ToList<CardModel>();
        int handSize = list.Count<CardModel>();
        int num = 10 - CardPile.GetCards(knifeGun.Owner, PileType.Hand).Count<CardModel>();

        await Cmd.CustomScaledWait(0.0f, 0.25f);
        IEnumerable<CardModel> inHand = await Shiv.CreateInHand(knifeGun.Owner, DynamicVars.Cards.IntValue, knifeGun.CombatState);
        if (!knifeGun.IsUpgraded)
            return;
        foreach (CardModel card in inHand)
            CardCmd.Upgrade(card);

    }




}