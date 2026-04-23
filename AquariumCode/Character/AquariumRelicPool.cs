using BaseLib.Abstracts;
using Aquarium.AquariumCode.Extensions;
using Godot;

namespace Aquarium.AquariumCode.Character;

public class AquariumRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => Aquarium.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}