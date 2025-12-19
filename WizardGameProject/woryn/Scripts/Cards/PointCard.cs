using Godot;
using System;
using System.Collections.Generic;

public partial class PointCard : Node, CardInterface
{
	public string CardName { get; private set; }
	public int PointValue { get; private set; }
	public CardRaritiesEnum CardRarity { get; private set; }
	private List<ModifierCard> ModifierList;
	public PointCard(string cardName, int pointValue, CardRaritiesEnum cardRarity)
	{
		CardName = cardName;
		PointValue = pointValue;
		CardRarity = cardRarity;
		ModifierList = new List<ModifierCard>();
	}

	public bool AddModifier(ModifierCard card)
	{
		return false;
	}

	public void RemoveModifier(ModifierCard card)
	{
		
	}
}
