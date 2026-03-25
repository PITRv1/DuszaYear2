using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class LobbyManager : Node
{
	private Dictionary<byte, string> players;
	public LobbyManager()
	{
		players = new();
		Global.lobbyManagerInstance = this;
	}

	public void AddToMultiplayerList(int id, string name)
	{
		players.Add((byte)id, name);

		GD.Print($"Adding player bruhhhh to lobby");

		var packet = new NewPlayer
		{
			playerArray = players.Values.ToArray(),
		};
		
		foreach (int player in players.Keys)
		{
			GD.Print($"Adding player {player} to lobby");
			Global.networkHandler.ClientPeers.TryGetValue(player, out var peer);

			if (peer != null)
				packet.Send(peer);
		}
	}

	public void RemoveFromMultiplayerList(int id)
	{
		GD.Print($"Removing player {id} from multiplayer list");
		players.Remove((byte)id);
		
		var packet = new NewPlayer
		{
			playerArray = players.Values.ToArray(),
		};
		
		foreach (int player in players.Keys)
		{
			GD.Print($"Adding player {player} to lobby");
			Global.networkHandler.ClientPeers.TryGetValue(player, out var peer);

			if (peer != null)
				packet.Send(peer);
		}
	}

	public void ResetPlayList()
	{
		GD.Print("SEVER IS SOTEP");
		players.Clear();
	}
	
	public void StartGameRequest(byte[] data)
	{	
		StartGame packet = new StartGame
		{
			senderId = 0,
			playerCount = players.Count
		};

		foreach (int player in players.Keys)
		{
			Global.networkHandler.ClientPeers.TryGetValue(player, out var peer);

			if (peer != null)
				packet.Send(peer);
		}
	}
}
