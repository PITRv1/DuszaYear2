using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class MultiplayerPlayerClass : Node
{
    public PlayerClass playerClass;
    public int ID;
    List<int> ids = new();

    [Export] HBoxContainer pointCards;
    [Export] HBoxContainer modifCards;
    [Export] PackedScene pointCardUI;
    
    public override void _Ready()
    {
        Global.multiplayerClientGlobals.HandleLocalIdAssignment += Local;
        Global.multiplayerClientGlobals.HandleRemoteIdAssignment += Remote;
        Global.networkHandler.OnPeerConnected += Remote;

        Global.multiplayerClientGlobals.HandleTurnInfo += playerClass.ProccessTurnInfoPacket;
        Global.multiplayerClientGlobals.HandlePickUpCardAnswer += playerClass.ProccessPickUpAnswer;
    }

    public MultiplayerPlayerClass()
    {
        playerClass = new PlayerClass();
        playerClass.parent = this;
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
        if (!playerClass.CanEndTurn())
            return;
        EndTurnRequest packet = new EndTurnRequest
        {
            PointCards = playerClass.PointCardList.ToArray(),
            ModifierCards = playerClass.ModifCardList.ToArray(),
        };

        Global.networkHandler._serverPeer?.Send(0, packet.Encode(), (int)ENetPacketPeer.FlagReliable);
    }

    public void PickUpCards()
    {
        PickUpCardRequest packet = new PickUpCardRequest
        {
            SenderId = ID,
        };

        Global.networkHandler._serverPeer?.Send(0, packet.Encode(), (int)ENetPacketPeer.FlagReliable);
    }

    public void AddPointToContainer(int pointValue)
    {
        TestPointCardUi test = pointCardUI.Instantiate() as TestPointCardUi;
        test.text.Text = pointValue.ToString();
        pointCards.AddChild(test);
    }
}
