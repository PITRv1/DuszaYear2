using Godot;
using System;
using System.Collections.Generic;

public partial class TurnManager : Node
{
	private int currentMaxValue = 0;
	private int pointCardValue;
	private List<ModifierCard> modifierCardsPlayed;
	private ModifierCardDeck modifierCardDeck;
	private PointCardDeck pointCardDeck;
	private int currentPlayer;
	private int playerCount = 2;
	private List<MultiplayerPlayerClass> players;

	public override void _Ready()
	{
		modifierCardsPlayed = new List<ModifierCard>();

		modifierCardDeck = new ModifierCardDeck();
		pointCardDeck = new PointCardDeck();
		
		modifierCardDeck.GenerateDeck();
		pointCardDeck.GenerateDeck();
		
		Global.turnManagerInstance = this;
	}

	private void GeneratePlayers()
	{
		
	}

	public void AddToMultiplayerList(int id)
	{
		GD.Print("BWA");
		MultiplayerPlayerClass newPlayer = new MultiplayerPlayerClass
		{
			ID = id,
		};
		if (players == null)
			players = new List<MultiplayerPlayerClass>();
		players.Add(newPlayer);
	}

	private void GetRandomPlayer()
	{
		RandomNumberGenerator rng = new RandomNumberGenerator();
		currentPlayer = rng.RandiRange(0, playerCount);
	}

	public void SetPointCardValue(int value)
	{
		pointCardValue = value;
	}

	public void AddCardToModifierCards(ModifierCard card)
	{
		modifierCardsPlayed.Add(card);
	}

	public void RemoveFromModifierCards(ModifierCard card)
	{
		modifierCardsPlayed.Remove(card);
	}

	private int CalculateCardValue()
	{
		int value;

		value = pointCardValue;

		foreach (ModifierCard modifierCard in modifierCardsPlayed)
			value = modifierCard.Calculate(value);

		return value;
	}

	public void PickUpCards()
	{
		int count = 4 < pointCardDeck.GetCount() ? 4 : pointCardDeck.GetCount();
		// ModifierCard[] modifierCards = modifierCardDeck.PullCards(4);
		PointCard[] pointCards = pointCardDeck.PullCards(count);

		foreach (PointCard pointCard in pointCards)
		{
			GD.Print(pointCard.PointValue);
		}
		pointCardDeck.PrintCards();
	}

	public void EndRound()
	{
		if (currentMaxValue < CalculateCardValue())
		{
			currentMaxValue = CalculateCardValue();
			GD.Print("LMAOO");
		}
		else
		{
			GD.Print("KILL YOURSELF");
			currentMaxValue = 0;
		}
	}

	public void ProccessTurnInfo(TurnInfoPacket packet)
	{
		BroadcastCurrentTurn();
	}

    private void BroadcastCurrentTurn()
	{
		TurnInfoPacket packet = new TurnInfoPacket
		{

		};

		foreach (var player in players)
		{
			Global.networkHandler._clientPeers.TryGetValue(player.ID, out var peer);
			if (peer != null)
			{
				packet.Send(peer);
			}
		}
	}

}
