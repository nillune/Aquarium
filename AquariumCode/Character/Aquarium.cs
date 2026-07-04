using Aquarium.AquariumCode.Cards.Basic;
using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using Aquarium.AquariumCode.Extensions;
using Aquarium.AquariumCode.Relics;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace Aquarium.AquariumCode.Character;



public  class Aquarium : PlaceholderCharacterModel
{
    public const string CharacterId = "Aquarium";

    public static readonly Color Color = new("0F2F60");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Feminine;
    public override int StartingHp => 70;
    
    public override Color EnergyLabelOutlineColor => new Color("0F2F37");

    public override Color DialogueColor => new Color("590700");

    

    public override Color MapDrawingColor => new Color("0F2F60");

    public override Color RemoteTargetingLineColor => new Color("0F2F60");

    public override Color RemoteTargetingLineOutline => new Color("0F2F37");

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeAquarium>(),
        ModelDb.Card<StrikeAquarium>(),
        ModelDb.Card<StrikeAquarium>(),
        ModelDb.Card<StrikeAquarium>(),
        ModelDb.Card<DefendAquarium>(),
        ModelDb.Card<DefendAquarium>(),
        ModelDb.Card<DefendAquarium>(),
        ModelDb.Card<DefendAquarium>(),
        ModelDb.Card<Bubble>(),
        ModelDb.Card<Blaster>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<DecroratedBowl>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<AquariumCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<AquariumRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<AquariumPotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomCharacterSelectBg => "res://Aquarium/scenes/charselectbg/charselectbg.tscn";
    public override string CustomIconTexturePath => "res://Aquarium/images/ui/character_icon.png";
    public override string CustomCharacterSelectIconPath => "res://Aquarium/images/ui/CharacterSelectIcon.png";
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();

    public override string CustomEnergyCounterPath => "res://Aquarium/images/charui/aquarium_energy_counter.tscn";
    public override string CustomMapMarkerPath => "res://Aquarium/images/ui/map_marker.png";
    public override string CustomArmPointingTexturePath  => "res://Aquarium/images/arms/defaulthand.png";
    public override string CustomArmPaperTexturePath  => "res://Aquarium/images/arms/paperhand.png";
    public override string CustomArmScissorsTexturePath  => "res://Aquarium/images/arms/scissors.png";
    public override string CustomArmRockTexturePath  => "res://Aquarium/images/arms/rock.png";
    public override string CustomMerchantAnimPath  => "res://Aquarium/scenes/shopidle.tscn";

    public override string CustomRestSiteAnimPath => "res://Aquarium/scenes/restsite.tscn";
    public override string CustomVisualPath => "res://Aquarium/scenes/combatanims/aquarium.tscn";
}