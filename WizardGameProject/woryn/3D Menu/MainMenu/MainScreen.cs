using Godot;
using System;

public partial class MainScreen : Control
{
    [Export] AnimationPlayer animPlayer;
    bool firstEnter = false;
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey eventKey && firstEnter == false)
            animPlayer.Play("intro");
            firstEnter = true;
    }
}
