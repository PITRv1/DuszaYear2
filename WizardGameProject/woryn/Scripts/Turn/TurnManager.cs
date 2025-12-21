using Godot;
using System;
using System.Collections.Generic;

public partial class TurnManager
{
	private int currentMaxValue = 0;
	private int pointCardValue;
	private List<ModifierCard> modifierCardsPlayed;
	private ModifierCardDeck modifierCardDeck;
	private PointCardDeck pointCardDeck;
	private int currentPlayer;
	private int playerCount = 2;
	private Dictionary<int, MultiplayerPlayerClass> players;

	public TurnManager()
	{
		GD.Print("yo??");
		modifierCardsPlayed = new List<ModifierCard>();

		modifierCardDeck = new ModifierCardDeck();
		pointCardDeck = new PointCardDeck();
		
		modifierCardDeck.GenerateDeck();
		pointCardDeck.GenerateDeck();

		
		// Global.turnManagerInstance = this;
		PrepareGame();
	}

	public void PrepareGame()
	{
		GetRandomPlayer();
	}

	public void AddToMultiplayerList(int id)
	{
		GD.Print("BWA");
		MultiplayerPlayerClass newPlayer = new MultiplayerPlayerClass
		{
			ID = id,
		};
		if (players == null)
			players = new Dictionary<int, MultiplayerPlayerClass>();
		players.Add(id, newPlayer);
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

	public void PickUpCards(int id)
	{
		if (currentPlayer != id)
		{
			GD.Print("NOT YOUR TURN");
			return;
		}

		PlayerClass playerClass = players[id].playerClass;
		PointCard[] pointCards;
		ModifierCard[] modifierCards;

		pointCardDeck.PrintCards();

		pointCards = pointCardDeck.PullCards(playerClass.PointCardList.Count);
		modifierCards = modifierCardDeck.PullCards(playerClass.ModifCardList.Count);

		playerClass.PointCardList.AddRange(pointCards);
		playerClass.ModifCardList.AddRange(modifierCards);

		PickUpCardAnswer packet = new PickUpCardAnswer
		{
			PointCards = pointCards,
			ModifierCards = modifierCards,
		};

		Global.networkHandler._clientPeers.TryGetValue(id, out var peer);

		if (peer != null)
			packet.Send(peer);
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

		foreach (int player in players.Keys)
		{
			Global.networkHandler._clientPeers.TryGetValue(player, out var peer);
			if (peer != null)
			{
				packet.Send(peer);
			}
		}
	}

}
