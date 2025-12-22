using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TurnManager
{
	private int currentMaxValue = 0;
	// private List<ModifierCard> modifierCardsPlayed;
	private ModifierCardDeck modifierCardDeck;
	private PointCardDeck pointCardDeck;
	private int currentPlayer;
	private int playerCount = 2;
	private int CurrentRound = 1;
	private Dictionary<int, MultiplayerPlayerClass> players;

	public TurnManager()
	{
		GD.Print("yo??");
		// modifierCardsPlayed = new List<ModifierCard>();

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
		currentPlayer = rng.RandiRange(0, playerCount - 1);
	}

	// public void SetPointCardValue(int value)
	// {
	// 	pointCardValue = value;
	// }

	// public void AddCardToModifierCards(ModifierCard card)
	// {
	// 	modifierCardsPlayed.Add(card);
	// }

	// public void RemoveFromModifierCards(ModifierCard card)
	// {
	// 	modifierCardsPlayed.Remove(card);
	// }

	private int CalculateCardValue(int value, ModifierCard[] cards)
	{
		foreach (ModifierCard modifierCard in cards)
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

	// public void EndRound()
	// {
	// 	if (currentMaxValue < CalculateCardValue())
	// 	{
	// 		currentMaxValue = CalculateCardValue();
	// 		GD.Print("LMAOO");
	// 	}
	// 	else
	// 	{
	// 		GD.Print("KILL YOURSELF");
	// 		currentMaxValue = 0;
	// 	}
	// }

	private bool DoesPlayerOwnModifiers(ModifierCard[] cards)
	{
		List<MODIFIER_TYPES> types = ModifierCardTypeConverter.ClassListToTypeList(players[currentPlayer].playerClass.ModifCardList);
		foreach (ModifierCard card in cards)
		{
			if (!types.Contains(card.ModifierType))
				return false;
		}
		return true;
	}

	private List<int> GetCardListValues(List<PointCard> cards)
	{
		List<int> values = new List<int>();

		foreach (PointCard card in cards)
		{
			values.Add(card.PointValue);
		}

		return values;
	}

	private void StartNewTurn(PointCard pointCard, ModifierCard[] modifierCards)
	{
		int lastPlayer = currentPlayer;
		currentPlayer++;
		if (playerCount - 1 < currentPlayer)
			currentPlayer = 0;

		currentMaxValue = CalculateCardValue(pointCard.PointValue, modifierCards);

		foreach (int player in players.Keys)
		{

			TurnInfoPacket packet = new TurnInfoPacket
			{
				LastPlayer = lastPlayer,
				CurrentPlayerId = currentPlayer,
				CurrentRound = CurrentRound,
				MaxValue = currentMaxValue,
				CurrentPointValue = players[player].playerClass.Points
			};

			Global.networkHandler._clientPeers.TryGetValue(player, out var peer);
			if (peer != null)
			{
				packet.Send(peer);
			}
		}
	}

	public void ProccessEndGameRequest(byte[] data)
	{
		GD.Print("EDDIG");
		EndTurnRequest packet = EndTurnRequest.CreateFromData(data);

		GD.Print(currentPlayer + " --- " + packet.SenderId);

		if (currentPlayer != packet.SenderId)
			return;

		PlayerClass currPlayer = players[currentPlayer].playerClass;

		PointCard pointCard = packet.PointCard;
		ModifierCard[] modifierCards = packet.ModifierCards;

		GD.Print(packet.PointCardIndex + " " + currPlayer.PointCardList.Count);
		if (currPlayer.PointCardList[packet.PointCardIndex].PointValue != pointCard.PointValue)
			return;

		for (int i = 0; i < modifierCards.Length; i++)
			if (currPlayer.ModifCardList[packet.ModifCardIndexes[i]].ModifierType != modifierCards[i].ModifierType)
				return;

		currPlayer.PointCardList.RemoveAt(packet.PointCardIndex);

		List<byte> sortedIndexes = packet.ModifCardIndexes.ToList();
		sortedIndexes.Sort();
		sortedIndexes.Reverse();

		foreach (byte index in sortedIndexes)
		{
			currPlayer.ModifCardList.RemoveAt(index);			
		}

		StartNewTurn(pointCard, modifierCards);
	}

}
