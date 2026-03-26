using System.Collections.Generic;
using Godot;
using System.Linq;
using System;

public partial class MultiplayerPlayerClass : Node
{
	public PlayerClass PlayerClass;
	public int Id;
	public string PlayerName;
	// List<int> ids = new();

	[Export] private CardPlacementHandler _pointCards;
	[Export] private CardPlacementHandler _modifierCards;
	[Export] private PackedScene _pointCardUi;
	[Export] private PackedScene _modifierCardUi;
	[Export] private Node3D _playerSeatsHolder;
	[Export] private PackedScene _buddy;
	[Export] public PlayerHud _playerHud {private set; get;}
	[Export] private UiCommunicator uiCommunicator;
	[Export] private Deck3d deck3D;
	[Export] private PlayerVisualController _playerVisualController;
	private readonly Dictionary<int, PlayerVisualController> _playerVisuals = new Dictionary<int, PlayerVisualController>();
	[Export] private PackedScene _winScreenScene;

	
	public override void _Ready()
	{
		Id = Global.multiplayerClientGlobals.Id;
		Global.multiplayerClientGlobals.SetupPlace += Setup;
		Global.multiplayerClientGlobals.HandleTurnInfo += PlayerClass.ProcessTurnInfoPacket;
		Global.multiplayerClientGlobals.HandleTurnInfo += ShowGreen;
		Global.multiplayerClientGlobals.HandlePickUpCardAnswer += PlayerClass.ProcessPickUpAnswer;
		Global.multiplayerClientGlobals.HandleDeckSwap += PlayerClass.HandleDeckSwap;
		Global.multiplayerClientGlobals.ShopScene += uiCommunicator.StartShop;
		Global.multiplayerClientGlobals.HandleRoundSuccess += HandleRoundSuccess;
		Global.multiplayerClientGlobals.HandleLookAt += SetTargetPosition;
		Global.multiplayerClientGlobals.HandleGoldUpdate += SetGoldAmount;
		Global.multiplayerClientGlobals.HandleShopBuy += ShopBuy;
		Global.multiplayerClientGlobals.WinnerScreen += ShowWinScreen;
		
		Global.multiplayerPlayerClass = this;
		ClientReady();
	}

	private const string WHITE = "#ffffff";
	private const string GREEN = "#578b1d";

	private void ShowWinScreen(byte[] data)
	{
		var packet = WinnerScreenPacket.CreateFromData(data);

		var winScreen = _winScreenScene.Instantiate() as WinScreen;
		AddChild(winScreen);
		winScreen.SetPlayerNames(packet.names);
	}

	private void ShowGreen(byte[] data)
	{
		var packet = TurnInfoPacket.CreateFromData(data);
		_playerHud.timerLabel.Modulate = new Godot.Color(WHITE);
		foreach (var player in _playerVisuals.Values)
		{
			player.NamePlate.Modulate = new Godot.Color(WHITE);
		}
		if (packet.CurrentPlayerId == Id)
		{
			_playerHud.timerLabel.Modulate = new Godot.Color(GREEN);
		}
		else
		{
			_playerVisuals[packet.CurrentPlayerId].NamePlate.Modulate = new Godot.Color(GREEN);

		}
	}

	private void ShowGreen(int CurrPlayerId)
	{
		_playerHud.timerLabel.Modulate = new Godot.Color(WHITE);
		foreach (var player in _playerVisuals.Values)
		{
			player.NamePlate.Modulate = new Godot.Color(WHITE);
		}
		if (CurrPlayerId == Id)
		{
			_playerHud.timerLabel.Modulate = new Godot.Color(GREEN);
		}
		else
		{
			_playerVisuals[CurrPlayerId].NamePlate.Modulate = new Godot.Color(GREEN);

		}
	}

	private void ShopBuy(byte[] data)
	{
		var packet = ShopItemBuy.CreateFromData(data);
		if (packet.SenderId != Id)
		{
			uiCommunicator.shopCards.RemoveCard(uiCommunicator.shopCards.GetChild(packet.CardIndex) as Node3D, false);
			return;
		}
		GD.Print("Shop buy start: " + packet.IsPublicShop);
		if (packet.IsPublicShop == 1)
		{
			GD.Print("Na mi van");
			PlayerClass.UpgradeStats(packet.Item);
		}
		uiCommunicator.shopCards.RemoveCard(uiCommunicator.shopCards.GetChild(packet.CardIndex) as Node3D, false);
		PlayerClass.Gold = packet.GoldAmount;
		_playerHud.UpdateGoldAmount((int)PlayerClass.Gold);
	}

	private void SetGoldAmount(byte[] data)
	{
		var packet = GoldPacket.CreateFromData(data);
		PlayerClass.Gold += packet.GoldAmount;
		PlayerClass.Points -= packet.PointAmount;
		_playerHud.UpdatePointsAmount(PlayerClass.Points);
		_playerHud.UpdateGoldAmount((int)PlayerClass.Gold);
	}

	private void SetTargetPosition(byte[] data)
	{
		var packet = LookAtPacket.CreateFromData(data);
		if (Id == packet.PlayerId)
		{
			return;
		}

		if (_playerVisuals.TryGetValue(packet.PlayerId, out var value))
		{
            value.TargetMarker.Position = packet.TargetPosition;
		}
	}

	private void HandleRoundSuccess(byte[] data)
	{
		var packet = RoundSuccessPacket.CreateFromData(data);

		if (packet.PlayerId != Id)
		{
			return;
		}

		PlayerClass.ChosenPointCard = null;
		PlayerClass.ChosenModifierCards.Clear();
		
		var sortedPointCards = packet.DeletePointCards.OrderByDescending(x => x).ToList();
		var sortedModifierCards = packet.DeleteModifierCards.OrderByDescending(x => x).ToList();
		
		foreach (var cardIndex in sortedPointCards)
		{
			PlayerClass.PointCardList.RemoveAt(cardIndex);
		}

		foreach (var cardIndex in sortedModifierCards)
		{
			PlayerClass.ModifierCardList.RemoveAt(cardIndex);
		}
	}

	private void Setup(byte[] data)
	{
		var packet = SetupPacket.CreateFromData(data);
		var seats = _playerSeatsHolder.GetChildren();
		var playerCount = packet.players.Count;

		if (packet.players.Keys.ToArray().Length == 0)
		{
			if (packet.StarterPlayer == Id)
			{
				ShowGreen(packet.StarterPlayer);
				_playerHud.StartCountdownTimer();
			}
			return;
		}

		var seatMappings = new Dictionary<int, Dictionary<int, int>>()
		{
			{ 1, new Dictionary<int, int>() },                                          // solo — no other players
			{ 2, new Dictionary<int, int> { { 1, 2 } } },                              // one opponent, sits opposite
			{ 3, new Dictionary<int, int> { { 1, 0 }, { 2, 2 } } },                   // two opponents, flanking
			{ 4, new Dictionary<int, int> { { 1, 0 }, { 2, 2 }, { 3, 1 } } },        // three opponents, all sides
		};

		var playerToSeat = seatMappings[playerCount];
		var offset = Id;

		foreach (var player in packet.players.Keys)
		{
			if (player == Id) continue;

			var seatOffset = ((player - offset) % playerCount + playerCount) % playerCount;

			var playerSeat = playerToSeat[seatOffset];
			var bud = _buddy.Instantiate() as PlayerVisualController;
			bud.NamePlate.Text = packet.players[player] + " " + player;
			bud.PlayerIndex = 2;
			bud.SetColor();
			bud.Camera.ProcessMode = ProcessModeEnum.Disabled;
			bud.PlayerControlled = false;
			_playerVisuals.Add(player, bud);
			seats[playerSeat].AddChild(bud);
			bud.Position = new Vector3(0, -2f, 0);
			var tableCenter = new Vector3(0, bud.GlobalPosition.Y, 0);
			bud.LookAt(tableCenter, Vector3.Up);
			bud.RotateY(Mathf.Pi);
		}

		if (packet.StarterPlayer == Id)
		{
			_playerHud.StartCountdownTimer();
		}

		ShowGreen(packet.StarterPlayer);
	}

	private static void ClientReady()
	{
		var packet = new ClientReady();
		Global.networkHandler.ServerPeer?.Send(0, packet.Encode(), (int)ENetPacketPeer.FlagReliable);
	}

	public MultiplayerPlayerClass()
	{
		PlayerClass = new PlayerClass
		{
			Parent = this
		};
	}

	private void Local(int id)
	{
		Id = id;
	}

	public void SetUi(int mPoints, int plrPoints, int throwValue)
	{
		deck3D.UpdateTotalValueText(throwValue);
		deck3D.UpdateCurrentValueText(mPoints);
		_playerHud.UpdatePointsAmount(plrPoints);
	}

	public void PlayCard()
	{
		GD.Print("Play card called");
		if (!PlayerClass.CanEndTurn())
		{
			GD.Print("Player cannot end turn");			
			return;
		}

		var packet = new EndTurnRequest
		{
			SenderId = Id,
			PointCard = PlayerClass.ChosenPointCard,
			PointCardIndex = PlayerClass.PointCardList.IndexOf(PlayerClass.ChosenPointCard),
			ModifierCards = PlayerClass.ChosenModifierCards.ToArray(),
			ModifCardIndexes = PlayerClass.ChosenModifierCards.Select(card => (byte)PlayerClass.ModifierCardList.IndexOf(card)).ToArray()
		};

		Global.networkHandler.ServerPeer?.Send(0, packet.Encode(), (int)ENetPacketPeer.FlagReliable);
		_playerHud.StopCountdownTimer();
	}

	private void PickUpCards()
	{
		var packet = new PickUpCardRequest
		{
			SenderId = Id,
		};
		Global.networkHandler.ServerPeer?.Send(0, packet.Encode(), (int)ENetPacketPeer.FlagReliable);
	}

	private void PlayAbilityRequest()
	{
		PlayerClass.Active.PlayAbility();
	}

	public void SendFoldRequest()
	{
		var packet = new Fold
		{
			SenderId = Id	
		};

		Global.networkHandler.ServerPeer?.Send(0, packet.Encode(), (int)ENetPacketPeer.FlagReliable);
	}

	public void AddPointToContainer(PointCard pointCard)
	{
		if (_pointCardUi.Instantiate() is not PointCard3d card) return;
		card.PointCard = pointCard;
		
		_pointCards.AddCard(card);
	}

	public void AddModifierToContainer(ModifierCard modifierCard)
	{
		if (_modifierCardUi.Instantiate() is not ModifierCard3d card) return;
		card.ModifierCard = modifierCard;

		_modifierCards.AddCard(card);
	}

	public void ResetContainers()
	{
		uiCommunicator.selectedPointCard3D = null;
		uiCommunicator.selectedModifierCard3Ds.Clear();
		var count = _modifierCards.GetChildCount();
		for (var i = 0; i < count; i++)
		{
			_modifierCards.RemoveCard(_modifierCards.GetChild(0) as Node3D);
			// _modifierCards.RemoveCard();
		}

		count = _pointCards.GetChildCount();
		for (var i = 0; i < count; i++)
		{
			_pointCards.RemoveCard(_pointCards.GetChild(0) as Node3D);
		}

		foreach (var card in PlayerClass.PointCardList)
		{
			AddPointToContainer(card);
		}
		
		foreach (var card in PlayerClass.ModifierCardList)
		{
			AddModifierToContainer(card);
		}
	}
}
