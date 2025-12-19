using Godot;
using System.Collections.Generic;

public partial class MultiPlayerServerGlobals : Node
{
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
            .Create(peerId, new List<int>(_peerIds))
            .Broadcast(Globals.networkHandler._connection);
    }

    private void OnPeerDisconnected(int peerId)
    {
        _peerIds.Remove(peerId);
    }

    private void OnServerPacket(int peerId, byte[] data)
    {
        switch ((PacketInfo.PACKET_TYPE)data[0])
        {
            case PacketInfo.PACKET_TYPE.PLAYER_POSITION:
                GD.PushError($"Player position is unhandled due to dani kukáing it.");
                break;

            default:
                GD.PushError($"Packet type with index {data[0]} unhandled");
                break;
        }
    }
}
