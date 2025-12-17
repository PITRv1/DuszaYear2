using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class ModifierCard : Node, CardInterface
{
	public string CardName { get; private set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
