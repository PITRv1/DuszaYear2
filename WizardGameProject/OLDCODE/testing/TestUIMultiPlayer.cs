using Godot;
using System;

public partial class TestUIMultiPlayer : Control
{   
    [Export] PackedScene scene;

    public void Host()
    {
        Globals.networkHandler.StartServer();
        Visible = false;
    }
    
    public void Join()
    {
        Globals.networkHandler.StartClient();
        GetTree().ChangeSceneToPacked(scene);
    }
}
