using Godot;
using System;
using System.Collections.Generic;
using System.Data;

public partial class NetworkHandler : Node
{
    public NetworkHandler()
    {
        Global.networkHandler = this;
    }

    [Signal] public delegate void OnPeerConnectedEventHandler(int peerId);
    [Signal] public delegate void OnPeerDisconnectedEventHandler(int peerId);
    [Signal] public delegate void OnServerPacketEventHandler(int peerId, byte[] data);

    [Signal] public delegate void OnConnectedToServerEventHandler();
    [Signal] public delegate void OnDisconnectedFromServerEventHandler();
    [Signal] public delegate void OnClientPacketEventHandler(byte[] data);


    private Stack<int> _availablePeerIds = new();
    public Dictionary<int, ENetPacketPeer> _clientPeers = new();

    public ENetPacketPeer _serverPeer;
    public ENetConnection _connection;

    private bool _isServer = false;


    public override void _Ready()
    {
        for (int i = 255; i >= 0; i--)
            _availablePeerIds.Push(i);

        GD.Print("Network Handler ready!");
    }

    public override void _Process(double delta)
    {
        if (_connection == null) return;
        HandleEvents();
    }

    private void HandleEvents()
    {
        Godot.Collections.Array packetEvent = _connection.Service();
        int netEvent = (int)packetEvent[0];

        if (netEvent == (int)ENetConnection.EventType.None) return;
        
        ENetPacketPeer Peer = (ENetPacketPeer)packetEvent[1];

        switch (netEvent)
        {
            case (int)ENetConnection.EventType.Error:
                GD.PushWarning("NetworkHandler: Error occurred");
                break;

            case (int)ENetConnection.EventType.Connect:
                if (_isServer)
                    PeerConnected(Peer);
                else
                    ConnectedToServer();
                break;

            case (int)ENetConnection.EventType.Disconnect:
                if (_isServer)
                    PeerDisconnected(Peer);
                else
                
                    DisconnectedFromServer();
                break;

            case (int)ENetConnection.EventType.Receive:
                if (_isServer)
                {
                    int peerId = (int)Peer.GetMeta("id");
                    EmitSignal(SignalName.OnServerPacket, peerId, Peer.GetPacket());
                }
                else
                    EmitSignal(SignalName.OnClientPacket, Peer.GetPacket());
                break;
        }
        
    }

    public void StartServer(string ipAddress = "127.0.0.1", int port = 6767)
    {
        _connection = new ENetConnection();
        Error error = _connection.CreateHostBound(ipAddress, port);
        
        if (error != Error.Ok)
        {
            GD.PrintErr("Server failed to start: ", error);
            _connection = null;
            return;
        }

        _isServer = true;
        GD.Print("Server started");
    }

    private void PeerConnected(ENetPacketPeer peer)
    {
        int peerId = _availablePeerIds.Pop();
        peer.SetMeta("id", peerId);
        _clientPeers[peerId] = peer;

        GD.Print("Peer connected with id: ", peerId);
        EmitSignal(SignalName.OnPeerConnected, peerId);
    }

    private void PeerDisconnected(ENetPacketPeer peer)
    {
        int peerId = (int)peer.GetMeta("id");

        _availablePeerIds.Push(peerId);
        _clientPeers.Remove(peerId);

        GD.Print("Client ", peerId, " disconnected");
        EmitSignal(SignalName.OnPeerDisconnected, peerId);
    }

    public void StartClient(string ipAddress = "127.0.0.1", int port = 6767)
    {
        _connection = new ENetConnection();
        Error error = _connection.CreateHost(1);

        if (error != Error.Ok)
        {
            GD.PrintErr("Client failed to connect: ", error);
            _connection = null;
            return;
        }

        _serverPeer = _connection.ConnectToHost(ipAddress, port);
        GD.Print("Client connecting...");
    }

    public void DisconnectClient()
    {
        if (_isServer || _serverPeer == null)
            return;

        _serverPeer.PeerDisconnect();
    }

    private void ConnectedToServer()
    {
        GD.Print("Connected to server");
        EmitSignal(SignalName.OnConnectedToServer);
    }

    private void DisconnectedFromServer()
    {
        GD.Print("Disconnected from server");
        EmitSignal(SignalName.OnDisconnectedFromServer);
        _connection = null;
    }
}
