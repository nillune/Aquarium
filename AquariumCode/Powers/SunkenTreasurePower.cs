using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using Aquarium.AquariumCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;


namespace Aquarium.AquariumCode.Powers;

  
public class SunkenTreasurePower : CustomPowerModel
{

    

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
}