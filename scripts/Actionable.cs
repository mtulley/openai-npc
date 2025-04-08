using Godot;
using System;
using DialogueManagerRuntime;
using System.ComponentModel.DataAnnotations.Schema;

public partial class Actionable : Area2D
{
    public Godot.Resource dialogue;

	public void Action(){
		dialogue = GD.Load<Resource>("res://dialogue/cow.dialogue");
		DialogueManager.ShowDialogueBalloon(dialogue, "start");	
	}
}
