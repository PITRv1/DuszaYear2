using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class MultiplayerPlayerClass : Node
{
    public PlayerClass playerClass;
    public int ID;
    List<int> ids = new();
    

    public override void _Ready()
    {
        Global.multiplayerClientGlobals.HandleLocalIdAssignment += Local;
        Global.multiplayerClientGlobals.HandleRemoteIdAssignment += Remote;
        Global.networkHandler.OnPeerConnected += Remote;
        Global.networkHandler.OnClientPacket += OnClientPacket;
    }

    private void Local(int id)
    {
        ID = id;

        GD.Print("Local id set to: ", id);
    }

    private void Remote(int id)
    {
        ids.Add(id);

        GD.Print(ID, $" -- ({ids.Count}) -> ", id);
    }

    public void PlayCard()
    {
        TurnInfoPacket packet = new TurnInfoPacket
        {
            
        };

        Global.networkHandler._serverPeer?.Send(0, packet.Encode(), (int)ENetPacketPeer.FlagReliable);
    }

    private void OnClientPacket(byte[] data)
    {
        PACKET_TYPES type = (PACKET_TYPES)data[0];
        switch (type)
        {
            case PACKET_TYPES.TURN_INFO:
                GD.Print("yay " + ID);
                break;
        }
    }
}
