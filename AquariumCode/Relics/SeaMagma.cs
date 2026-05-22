using Aquarium.AquariumCode.Character;
using Aquarium.AquariumCode.Relics;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Relics;

  
[Pool(typeof(AquariumRelicPool))]
public class SeaMagma() : AquariumRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Uncommon;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar (1) ];
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
       
        if (this.Owner.Creature.HasPower<FrailPower>())
            await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, this.Owner );
        this.Flash();
    }
    
}