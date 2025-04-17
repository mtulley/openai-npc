using Godot;
using System;

public partial class Textbox : CanvasLayer
{
	MarginContainer marginContainer;
	Label label;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<MarginContainer>("MarginContainer");
		GetNode<Label>("Label");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
