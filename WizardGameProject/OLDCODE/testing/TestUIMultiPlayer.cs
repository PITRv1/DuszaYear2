using Godot;
using System;

public partial class TestUIMultiPlayer : Control
{   
    [Export] PackedScene scene;

    public void Host()
    {
        Global.networkHandler.StartServer();
        Visible = false;
    }
    
    public void Join()
    {
        Global.networkHandler.StartClient();
        GetTree().ChangeSceneToPacked(scene);
    }
}
