using Godot;
using System;

public partial class Buttons : VBoxContainer
{
	[Export] Button singleplayer;
    [Export] Button multiplayer;
    [Export] Button settings;
    [Export] Button credits;
    [Export] Button exit;

	[Export] PackedScene singleplayerScene;
    [Export] PackedScene multiplayerScene;
    [Export] PackedScene settingsScene;
    [Export] PackedScene creditsScene;


    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}


    private void OnSingleplayerPressed()
    {
        GetTree().ChangeSceneToPacked(singleplayerScene);
    }
    private void OnMultiplayerPressed()
	{
		GetTree().ChangeSceneToPacked(multiplayerScene);
	}
    private void OnSettingsPressed()
    {
        GetTree().ChangeSceneToPacked(settingsScene);
    }
    private void OnCreditsPressed()
    {
        GetTree().ChangeSceneToPacked(creditsScene);
    }
    private void OnExitPressed() 
    {
        GetTree().Quit();
    }
}
