using Godot;
using System;

public partial class Back : Button
{
	public override void _Ready()
	{ 
	}

	public void OnBackPressed()
	{

		GetTree().ChangeSceneToFile(@"res://UI/MainMenu/MainMenu.tscn");

    }
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
