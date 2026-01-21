using Godot;
using System;
using System.Net;
using System.Net.Sockets;

public partial class MainMenuController : Node3D
{
    // Reference to the AnimationTree node
    [Export]
    public AnimationTree AnimationTree;

    // Path to the AnimationNodeStateMachine inside the AnimationTree
    [Export]
    public StringName StateMachinePath = new StringName("parameters/BootomGear/playback");

    private Camera3D _mainMenuCam;
    private Camera3D _introCam;
    private RichTextLabel _richTextLabel;

    private AnimationNodeStateMachinePlayback _playback;
    private bool hasStarted = false;
    public override void _Ready()
    {
        _mainMenuCam = GetNode<Camera3D>("MainMenuCam");
        _introCam = GetNode<Camera3D>("IntroCam");
        _richTextLabel = GetNode<RichTextLabel>("RichTextLabel");

        if (AnimationTree == null)
        {
            GD.PushError("AnimationTree is not assigned.");
            return;
        }

        // Ensure the AnimationTree is active
        AnimationTree.Active = true;

        // Get the state machine playback object
        _playback = (AnimationNodeStateMachinePlayback)AnimationTree.Get(StateMachinePath);

        if (_playback == null)
        {
            GD.PushError("Failed to get AnimationNodeStateMachinePlayback. Check the state_machine_path.");
            return;
        }
    }


    // -----------------------------
    // Viewpoint transition methods
    // -----------------------------

    public override void _UnhandledInput(InputEvent @event)
    {
        if (hasStarted)
            return;

        if (@event.IsPressed())
        {
            hasStarted = true;
            _playback.Travel("intro");
        }
    }

    public void GoToMainViewPoint()
    {
        _playback.Travel("mainViewPoint");
    }

    public void GoToBarViewPoint()
    {
        _playback.Travel("barViewPoint");
    }

    public void GoToShelfViewPoint()
    {
        _playback.Travel("shelfViewPoint");
    }

    public void GoToPlayViewPoint()
    {
        _playback.Travel("playViewPoint");
    }

    public void ChangeCam()
    {
        _introCam.ClearCurrent();
        _mainMenuCam.MakeCurrent();

        _richTextLabel.QueueFree();
    }
}
