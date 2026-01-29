using Godot;
using System;
using System.Net;
using System.Net.Sockets;

public partial class MainMenuController : Node3D
{
    // Reference to the AnimationTree node
    [Export] private AnimationPlayer introAnimator;
    [Export] private RichTextLabel _gameStartHint;

    private bool hasStarted = false;

    public override void _UnhandledInput(InputEvent @event)
    {
        if (hasStarted)
            return;

        if (@event.IsPressed())
        {
            hasStarted = true;
            introAnimator.Play("intro");
        }
    }
}
