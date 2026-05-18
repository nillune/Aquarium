using Aquarium.AquariumCode.Cards.Rare;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;


namespace Aquarium.AquariumCode.Powers;

#nullable enable


public class PartTheSeaPowerLoss : TemporaryStrengthPower
{

   

    public override AbstractModel OriginModel => (AbstractModel) ModelDb.Card<PartTheSea>();

    protected override bool IsPositive => false;
}