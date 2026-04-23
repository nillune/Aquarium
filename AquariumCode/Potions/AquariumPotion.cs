using BaseLib.Abstracts;
using BaseLib.Utils;
using Aquarium.AquariumCode.Character;

namespace Aquarium.AquariumCode.Potions;

[Pool(typeof(AquariumPotionPool))]
public abstract class AquariumPotion : CustomPotionModel;