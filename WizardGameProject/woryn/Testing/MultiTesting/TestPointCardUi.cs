using Godot;
using System;

public partial class TestPointCardUi : Control
{
	[Export] public Label text;
	public PointCard pointCard;
	public PlayerClass playerClass;

	public void HandleSelection()
	{
		playerClass.chosenPointCard = pointCard;
		GD.Print(playerClass.chosenPointCard);
	}
}
