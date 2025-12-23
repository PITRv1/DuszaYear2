using Godot;
using System;

public partial class TestUIMultiPlayer : Control
{   
    [Export] PackedScene scene;

    public async void Host()
    {
        Global.networkHandler.StartServer();
        // await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
        Global.networkHandler.StartClient();
        GetTree().ChangeSceneToPacked(scene);
        // Visible = false;
    }
    
    public void Join()
    {
        Global.networkHandler.StartClient();
        GetTree().ChangeSceneToPacked(scene);
    }
}
