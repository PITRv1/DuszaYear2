using Godot;
using System;

public partial class TestModifierCardUi : Panel
{
	[Export] public Label text;
	public ModifierCard modifierCard;
	public PlayerClass playerClass;

	public void HandleSelection()
	{
		if (playerClass.chosenModifierCards.Contains(modifierCard))
		{
			playerClass.chosenModifierCards.Remove(modifierCard);
			GD.Print("Removed modifier --> ", this);
		} else
		{
			playerClass.chosenModifierCards.Add(modifierCard);
			GD.Print("Added modifier --> ", this);
		}
	}
}
