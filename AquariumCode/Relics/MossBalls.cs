using Aquarium.AquariumCode.Character;
using Aquarium.AquariumCode.Relics;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Relics;

 
  
[Pool(typeof(AquariumRelicPool))]
public class MossBalls() : AquariumRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Rare;
    protected override IEnumerable<DynamicVar> CanonicalVars => [ new PowerVar<VigorPower>(5)];
    public override async Task BeforeSideTurnEnd(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IEnumerable<Creature> participants)
    {

        if (!this.Owner.Creature.HasPower<VigorPower>())
        {
            await PowerCmd.Apply<VigorPower>(
                new ThrowingPlayerChoiceContext(),   Owner.Creature,
                DynamicVars[nameof(VigorPower)].BaseValue,
                Owner.Creature,
                null);
            this.Flash();
        }
    }
}