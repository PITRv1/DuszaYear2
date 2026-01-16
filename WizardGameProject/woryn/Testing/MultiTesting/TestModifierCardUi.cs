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
			RemoveCard();
			// GD.Print("Removed modifier --> ", this);
		} else
		{
			if (playerClass.AddToChosenModifierCards(modifierCard))
				SelectCard();
			// GD.Print("Added modifier --> ", this);
		}
	}

	public void RemoveCard()
	{
		Modulate = new Color(37, 37, 37);
	}

	public void SelectCard()
	{
		Modulate = new Color(55, 23, 52);
	}

}
