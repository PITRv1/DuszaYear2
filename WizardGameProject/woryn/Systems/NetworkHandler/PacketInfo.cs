using Godot;
using System;

public partial class PacketInfo : Node
{
    public enum PACKET_TYPE
    {
        ID_ASSIGNMENT = 0,
        PLAYER_POSITION = 1,
    }

    public PACKET_TYPE PacketType;
    public long Flag;

    public byte[] Encode()
    {
        byte[] data = [(byte)PacketType];
        return data;
    }

    public void Decode(byte[] data)
    {
        PacketType = (PACKET_TYPE)data[0];
    }

    public void Send(ENetPacketPeer target)
    {
        target.Send(0, Encode(), (int)Flag);
    }

    public void Broadcast(ENetConnection server)
    {
        server.Broadcast(0, Encode(), (int)Flag);
    }
}
