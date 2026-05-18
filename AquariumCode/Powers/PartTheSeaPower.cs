using Aquarium.AquariumCode.Cards.Rare;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Powers;

#nullable enable


public class PartTheSeaPower : TemporaryStrengthPower
{
    public override AbstractModel OriginModel => (AbstractModel) ModelDb.Card<PartTheSea>();

    
}
