using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace Aquarium.AquariumCode.Character;

[GlobalClass]
public partial class AquariumAnimation : Node2D
{
	private AnimatedSprite2D sprite;

	public override void _Ready()
	{
		base._Ready();
		sprite = GetNode<AnimatedSprite2D>("%Visuals");
		
	
	}

	
	
}
