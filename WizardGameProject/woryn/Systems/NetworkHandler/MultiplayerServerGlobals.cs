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
    }

    private void OnPeerConnected(int peerId)
    {
        _peerIds.Add(peerId);

        IDAssignment
            .Create(peerId, _peerIds)
            .Broadcast(Global.networkHandler._connection);
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
            default:
                GD.PushError($"Packet type with index {data[0]} unhandled");
                break;
        }
    }
}
