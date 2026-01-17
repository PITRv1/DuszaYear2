using Godot;
using System;

public partial class TestModifierCardUi : Control
{
	[Export] public RichTextLabel text;
	public ModifierCard modifierCard;
	public PlayerClass playerClass;
	bool isClicked = false;


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

	private void ChangeTextColor()
	{
		isClicked = !isClicked;

		if (isClicked) {
			text.Modulate = Colors.Purple;
			return;
		}
		
		text.Modulate = Colors.White;
	}

	public void RemoveCard()
	{
		Modulate = new Color(37 / 255f, 37 / 255f, 37 / 255f);
	}

	public void SelectCard()
	{
		Modulate = new Color(55f / 255f, 23f / 255f, 52f / 255f);
	}

}
