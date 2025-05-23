using Godot;
using System;
using System.Xml.Serialization;

public partial class Player : CharacterBody2D
{
	public int Speed = 50;
	private AnimatedSprite2D animatedSprite2D;
    Node areaParent;
    Area2D ActionableFinder;

    CanvasLayer TextBox;

    public void GetInput() 
    {
        var inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down") + Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
	    this.Velocity = inputDirection.Normalized() * Speed;
        ActionableFinder = GetNode<Area2D>("ActionableFinder");
    }

    public override void _UnhandledInput(InputEvent inputEvent)
    {
        if (Input.IsActionJustPressed("ui_accept"))
        {
            var actionables = ActionableFinder.GetOverlappingAreas();
            Actionable actionable = (Actionable)actionables[0];
            // actionable.Action();
            TextBox.Show();
        }
    }

    public override void _Ready()
	{
        // NOTE: CharacterBody2D.MotionMode is set to MOTION_MODE_FLOATING in the inspector

		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite2D.Play("idle_down");
        TextBox = GetParent().GetNode<CanvasLayer>("Textbox");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

    }

    public override void _PhysicsProcess(double delta)
    {
        GetInput();
        MoveAndSlide();
    }

    private void OnInteractionAreaEntered(Area2D area)
    {
        areaParent = area.GetParent();

        if (areaParent.IsInGroup("NPCs") && areaParent.GetNode<Label>("NameLabel") != null)
        {
            var nameLabel = areaParent.GetNode<Label>("NameLabel");
            nameLabel.Visible = true;
        }
    }

    private void OnInteractionAreaExited(Area2D area)
    {
        areaParent = area.GetParent();

        if (area.GetParent().IsInGroup("NPCs"))
        {
            area.GetParent().GetNode<Label>("NameLabel").Visible = false;
        }
    }
}

