using System.Reflection;
using BaseLib.Patches.Content;
using HarmonyLib;

namespace Aquarium.AquariumCode;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Runs.History;
using MegaCrit.Sts2.Core.TestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;


[HarmonyPatch(typeof(CardCmd), nameof(CardCmd.Exhaust))]
public static class CardCmdPatches
{
    [CustomEnum, KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword Weapon;
    
    static string _PreviousCard = "null";
    private static int _PreviousCardInt = 0;
    
    public static void Postfix(PlayerChoiceContext choiceContext, CardModel card)
    {
        // Only autoplay if the card has the Weapon keyword
        if (!card.Keywords.Contains(Weapon))
            return;
        if (_PreviousCard == card.Title)
            if (_PreviousCardInt == 1) 
                return;
        _PreviousCardInt = 1;
        _PreviousCard = card.Title;
        if(_PreviousCard != card.Title)
            _PreviousCardInt = 0;
        if (card.Keywords.Contains(CardKeyword.Ethereal))
                 card.AddKeyword(CardKeyword.Exhaust);
        card.ExhaustOnNextPlay = true;
        
        // Autoplay the card
        TaskHelper.RunSafely(CardCmd.AutoPlay(choiceContext, card, (Creature)null, AutoPlayType.Default));
    }
        [HarmonyPatch(typeof(CombatManager), nameof(CombatManager.IsEnding))]
    public static class CombatEndPatch
    {
        public static void Postfix()
        {
            _PreviousCard = "null";
            _PreviousCardInt = 0;
        }
    }
}
