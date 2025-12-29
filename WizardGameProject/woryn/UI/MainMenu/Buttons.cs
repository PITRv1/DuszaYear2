using Godot;
using System;

public partial class Buttons : Control
{
	[Export] Button singleplayer;
    [Export] Button multiplayer;
    [Export] Button settings;
    [Export] Button credits;
    [Export] Button exit;

    [Export] Button back;

    [Export] Control singleplayerScene;
    [Export] Control multiplayerScene;
    [Export] Control settingsScene;
    [Export] Control creditsScene;
    [Export] Control mainMenuScene;

    private Control[] scenes;

    // This is used only in class, thus must be private. The naming is confusing.
    // "Hider" is an adjective, methods are almost always verbs, like "Hide". Also, would be good
    // to know what will be hidden? Methods must talk to you by their names, you should not guess what it does.
    // 
    // For hide1-3, you can use [C# params](https://www.geeksforgeeks.org/c-sharp/c-sharp-params/)
    // And instead calling them hide1-3, you can use a more talkative variable name like:
    // params Control[] scenesToHide
    public void Hider(Control showable, Control hide1, Control hide2, Control hide3)
    {
        showable.Visible = true;
        hide1.Visible = false;
        hide2.Visible = false;
        hide3.Visible = false;

        back.Visible = true;
    }
    private void OnSingleplayerPressed()
    {
        back.Visible = true;
        mainMenuScene.Visible = false;
        Hider(singleplayerScene,multiplayerScene,settingsScene,creditsScene);
    }

    private void OnMultiplayerPressed()
    {
        back.Visible = true;
        mainMenuScene.Visible = false;
        Hider(multiplayerScene, singleplayerScene, settingsScene, creditsScene);
    }

    private void OnSettingsPressed()
    {
        back.Visible = true;
        mainMenuScene.Visible = false;
        Hider(settingsScene, multiplayerScene, singleplayerScene, creditsScene);
    }

    private void OnCreditsPressed()
    {
        back.Visible = true;
        mainMenuScene.Visible = false;
        Hider(creditsScene, multiplayerScene, settingsScene, singleplayerScene);
    }

    private void OnExitPressed()
    {
        GetTree().Quit();
    }

}
