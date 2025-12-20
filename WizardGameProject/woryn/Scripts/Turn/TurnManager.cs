using Godot;
using System;
using System.Collections.Generic;

public partial class TurnManager : Node
{
	private int currentMaxValue = 0;
	private int pointCardValue;
	private List<ModifierCard> modifierCards;

	public override void _Ready()
	{
		modifierCards = new List<ModifierCard>();
		GD.Print("APPLE");
	}

	public void SetPointCardValue(int value)
	{
		pointCardValue = value;
	}

	public void AddCardToModifierCards(ModifierCard card)
	{
		modifierCards.Add(card);
	}

	public void RemoveFromModifierCards(ModifierCard card)
	{
		modifierCards.Remove(card);
	}

	private int CalculateCardValue()
	{
		int value;

		value = pointCardValue;

		foreach (ModifierCard modifierCard in modifierCards)
			value = modifierCard.Calculate(value);

		return value;
	}

	public void EndRound()
	{
		if (currentMaxValue < CalculateCardValue())
		{
			currentMaxValue = CalculateCardValue();
		}
		else
		{
			GD.Print("KILL YOURSELF");
			currentMaxValue = 0;
		}
	}
}
