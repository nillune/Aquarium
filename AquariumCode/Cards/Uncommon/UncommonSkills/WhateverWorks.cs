using Aquarium.AquariumCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Cards.Uncommon;

  
public class WhateverWorks() : AquariumCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardKeyword.Exhaust ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get => new[] {   HoverTipFactory.FromKeyword(CardCmdPatches.Weapon)};
    }
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        foreach (CardModel card2 in CardFactory.GetForCombat(this.Owner,
                     this.Owner.Character.CardPool
                         .GetUnlockedCards(this.Owner.UnlockState, this.Owner.RunState.CardMultiplayerConstraint)
                         .Where<CardModel>((Func<CardModel, bool>)(c =>
                         {
                             return c.Keywords.Contains(CardCmdPatches.Weapon);
                         })), 1, this.Owner.RunState.Rng.CombatCardGeneration))
        {
            card2.SetToFreeThisTurn();
            CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(card2, PileType.Hand, this.Owner);
        }
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}