using Godot;
using System.Collections.Generic;

public partial class MultiplayerServerGlobals : Node
{
    public MultiplayerServerGlobals()
    {
        Global.multiplayerServerGlobals = this;
    }

    private List<int> _peerIds = new();

    public override void _Ready()
    {
        NetworkHandler network = Global.networkHandler;

        network.OnPeerConnected += OnPeerConnected;
        network.OnPeerDisconnected += OnPeerDisconnected;
        network.OnServerPacket += OnServerPacket;

        if (Global.turnManagerInstance == null)
            Global.turnManagerInstance = new TurnManager();
    }

    private void OnPeerConnected(int peerId)
    {
        _peerIds.Add(peerId);

        IDAssignment
            .Create(peerId, _peerIds)
            .Broadcast(Global.networkHandler.ServerConnection);

        if (Global.turnManagerInstance == null)
            Global.turnManagerInstance = new TurnManager();
        
        Global.turnManagerInstance.AddToMultiplayerList(peerId);
    }

    private void OnPeerDisconnected(int peerId)
    {
        _peerIds.Remove(peerId);
    }

    private void OnServerPacket(int peerId, byte[] data)
    {
        switch ((PACKET_TYPES)data[0])
        {
            case PACKET_TYPES.TURN_DATA:
                GD.PushError("Dani has no idea how we should handle this kind of packet.");
                break;
            case PACKET_TYPES.TURN_INFO:
                if (Global.turnManagerInstance == null)
                    Global.turnManagerInstance = new TurnManager();
                TurnInfoPacket turnPacket = TurnInfoPacket.CreateFromData(data);
                // Global.turnManagerInstance.ProccessTurnInfo(turnPacket);
                break;
            case PACKET_TYPES.PICK_UP_CARD_REQUEST:
                Global.turnManagerInstance.PickUpCards(peerId);
                break;
            case PACKET_TYPES.END_TURN_REQUEST:
                Global.turnManagerInstance.ProccessEndGameRequest(data);
                break;
            default:
                GD.PushError($"Packet type with index {(PACKET_TYPES)data[0]} unhandled");
                break;
        }
    }
}
