using Aquarium.AquariumCode.Cards.Rare;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Aquarium.AquariumCode.Powers;

public class CauldronOfEverythingPower: TemporaryStrengthPower
{

    


    public override AbstractModel OriginModel => (AbstractModel) ModelDb.Card<PartTheSea>();

    protected override bool IsPositive => false;
}