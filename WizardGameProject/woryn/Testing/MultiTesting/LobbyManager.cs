using Godot;
using System;
using System.Collections.Generic;

// Once I left a way too rude alert in a code which got unnoticed and went into production. In 5 mins I was in the
// management office. ALWAYS keep prints meaningful and nice. If you get used to this habit, you will save future yourself.
public partial class LobbyManager
{
	private List<byte> players;
	public LobbyManager()
	{
		GD.Print("bruh");
		players = new List<byte>();
		Global.lobbyManagerInstance = this;
	}

	public void AddToMultiplayerList(int id)
	{
		GD.Print("DWAOIUHDAIUDHAWIUODHDWUIOADHIUOAWD");
		players.Add((byte)id);

		NewPlayer packet = new NewPlayer
		{
			playerArray = players.ToArray(),
		};
		
		foreach (int player in players)
		{
			Global.networkHandler._clientPeers.TryGetValue(player, out var peer);

			if (peer != null)
				packet.Send(peer);
		}
	}

	public void StartGameRequest(byte[] data)
	{	
		StartGame packet = new StartGame
		{
			senderId = 0,
		};

		foreach (int player in players)
		{
			Global.networkHandler._clientPeers.TryGetValue(player, out var peer);

			if (peer != null)
				packet.Send(peer);
		}
	}
}
