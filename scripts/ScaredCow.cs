using Godot;
using System;

public partial class ScaredCow : CharacterBody2D
{
	AnimatedSprite2D animatedSprite2D;
	public Label name;
	Random random;
	private float Speed;

	private Vector2 direction;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Get references to important child nodes
		name = GetNode<Label>("NameLabel");
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		// Initialize variables
		random = new Random();
		Speed = 100;
		direction = Vector2.Zero;

		// Initialize
		animatedSprite2D.Play("idle");
		name.Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

    public override void _PhysicsProcess(double delta)
    {
        this.Velocity = direction.Normalized() * Speed;
		MoveAndSlide();
    }

	private void OnInteractionAreaEntered(Area2D area)
	{
		GD.Print("area entered");
		direction = new Vector2((random.Next(0, 2) == 1) ? 1 : -1, (random.Next(0, 2) == 1) ? 1 : -1);
		animatedSprite2D.Play("run");
	}

	private void OnInteractionAreaExited(Area2D area)
	{
		GD.Print("area exited");
		direction = Vector2.Zero;
		animatedSprite2D.Play("idle");
	}
}
