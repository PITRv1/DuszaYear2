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

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
