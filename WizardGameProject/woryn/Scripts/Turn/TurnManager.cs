using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TurnManager
{
	private int currentMaxValue = 0;
	// private List<ModifierCard> modifierCardsPlayed;
	private PointCardDeck pointCardDeck;
	private int currentPlayer;
	private int playerCount = 2;
	private int CurrentRound = 1;
	private Dictionary<int, MultiplayerPlayerClass> players;
	private int ThrowDeckValue = 0;

	public TurnManager(List<int> playerIds)
	{
		foreach (int id in playerIds)
		{
			AddToMultiplayerList(id);
		}

		playerCount = playerIds.Count;

		pointCardDeck = new PointCardDeck();
		
		pointCardDeck.GenerateDeck();
		
		Global.turnManagerInstance = this;
	}

	public void PrepareGame()
	{
		GetRandomPlayer();

		foreach (int player in players.Keys)
		{
			GD.Print("bruhererr " + player);
			DealCards(player);
		}
	}

	public void AddToMultiplayerList(int id)
	{
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
		foreach (ModifierCardMultiplier modifierCard in cards)
		{
			GD.Print("PLEASE SPEED: " + modifierCard.Amount);
			value = modifierCard.Calculate(value);
		}

		return value;
	}

	public void DealCards(int id)
	{
		PlayerClass playerClass = players[id].playerClass;
		PointCard[] pointCards;
		ModifierCard[] modifierCards;

		pointCardDeck.PrintCards();

		pointCards = pointCardDeck.PullCards(playerClass.PointCardList.Count);
		modifierCards = playerClass.modifierCardDeck.PullCards(playerClass.ModifCardList.Count);

		playerClass.PointCardList.AddRange(pointCards);
		playerClass.ModifCardList.AddRange(modifierCards);

		PickUpCardAnswer packet = new PickUpCardAnswer
		{
			PointCards = pointCards,
			ModifierCards = modifierCards,
		};

		Global.networkHandler._clientPeers.TryGetValue(id, out var peer);

		if (peer != null)
		{
			packet.Send(peer);
		}
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
		modifierCards = playerClass.modifierCardDeck.PullCards(playerClass.ModifCardList.Count);

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

	private bool DoPlayersHaveCards()
	{
		foreach (MultiplayerPlayerClass player in players.Values)
			if (player.playerClass.PointCardList.Count > 0)
				return true;

		return false;
	}

	private void GoToShopScene()
	{
		
	}

	private void StartNewTurn(PointCard pointCard, ModifierCard[] modifierCards, int value)
	{
		int lastPlayer = currentPlayer;
		currentPlayer++;
		if (playerCount - 1 < currentPlayer)
			currentPlayer = 0;

		if (pointCardDeck.GetCount() == 0 && !DoPlayersHaveCards())
		{
			GD.Print("Super over");
			ThrowDeckValue += value;
			players[lastPlayer].playerClass.Points += ThrowDeckValue;
			ThrowDeckValue = 0;
			currentMaxValue = 0;
			return;
		}

		if (CalculateCardValue(GetCardListValues(players[currentPlayer].playerClass.PointCardList).Max(), players[currentPlayer].playerClass.ModifCardList.ToArray()) <= value)
		{
			GD.Print("It's over");
			ThrowDeckValue += value;
			players[lastPlayer].playerClass.Points += ThrowDeckValue;
			ThrowDeckValue = 0;
			currentMaxValue = 0;
		}
		else
		{
			ThrowDeckValue += value;
			currentMaxValue = value;
		}

		foreach (int player in players.Keys)
		{

			TurnInfoPacket packet = new TurnInfoPacket
			{
				LastPlayer = lastPlayer,
				CurrentPlayerId = currentPlayer,
				CurrentRound = CurrentRound,
				MaxValue = currentMaxValue,
				CurrentPointValue = players[player].playerClass.Points,
				ThrowDeckValue = ThrowDeckValue
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

		GD.Print("MODIF CARD INDEXES: " + packet.ModifCardIndexes.Length);
		GD.Print("MODIF CARDs: " + modifierCards.Length);
		GD.Print("PLAYER MODIF CARDs: " + currPlayer.ModifCardList.Count);

		List<ModifierCard> usedCards = new List<ModifierCard>();

		for (int i = 0; i < modifierCards.Length; i++)
		{
			GD.Print("BUH: " + packet.ModifCardIndexes[i]);
			if (currPlayer.ModifCardList[packet.ModifCardIndexes[i]].ModifierType != modifierCards[i].ModifierType)
				return;
			usedCards.Add(currPlayer.ModifCardList[packet.ModifCardIndexes[i]]);
		}

		int turnValue = CalculateCardValue(pointCard.PointValue, usedCards.ToArray());

		if (turnValue <= currentMaxValue)
			return;

		currPlayer.PointCardList.RemoveAt(packet.PointCardIndex);

		List<byte> sortedIndexes = packet.ModifCardIndexes.ToList();
		sortedIndexes.Sort();
		sortedIndexes.Reverse();

		foreach (byte index in sortedIndexes)
		{
			currPlayer.ModifCardList.RemoveAt(index);			
		}

		PickUpCards(currentPlayer);

		StartNewTurn(pointCard, modifierCards, turnValue);
	}

}
