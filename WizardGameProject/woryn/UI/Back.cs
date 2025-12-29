using Godot;
using System;

// Just a few spare lines in the file.
public partial class Back : Button
{
    [Export] Control singleplayerScene;
    [Export] Control multiplayerScene;
    [Export] Control settingsScene;
    [Export] Control creditsScene;
    [Export] Control mainMenuScene;

    [Export] Button back;

    [Export] PackedScene mainMenuPacked;

    [Export] Node3D parent;


    public void OnBackPressed()
    {

        singleplayerScene.Visible = false;
        multiplayerScene.Visible = false;
        settingsScene.Visible = false;
        creditsScene.Visible = false;
        mainMenuScene.Visible = true;

        back.Visible = false;

    }

}
