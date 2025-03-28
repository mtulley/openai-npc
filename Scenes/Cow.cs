using Godot;
using System;

public partial class Cow : CharacterBody2D
{
	AnimatedSprite2D animatedSprite2D;
	public Label name;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		name = GetNode<Label>("NameLabel");
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play("idle");
		name.Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
