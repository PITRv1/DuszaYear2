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
    }

    private void Local(int id)
    {
        ID = id;

        GD.Print("Local id set to: ", id);
    }

    private void Remote(int id)
    {
        ids.Append(id);

        GD.Print(ID, " --> ", id);
    }

}
