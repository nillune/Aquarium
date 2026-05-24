using Aquarium.AquariumCode.Character;
using Aquarium.AquariumCode.Relics;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Aquarium.AquariumCode.Relics;

 
[Pool(typeof(AquariumRelicPool))]
public class BoiledTurnips() : AquariumRelic
{
	public override RelicRarity Rarity =>
		RelicRarity.Rare;

	public override decimal ModifyBlockMultiplicative(Creature target, decimal block, ValueProp props,
		CardModel? cardSource,
		CardPlay? cardPlay)
	{
		return target == this.Owner.Creature || this.Owner.Creature.HasPower<FrailPower>() ? 1.2M : 1M;
	}
}
