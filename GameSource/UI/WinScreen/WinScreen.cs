using Godot;
using System;

public partial class WinScreen : Control
{
    [Export] Godot.Collections.Array<Label> labels;
    [Export] PackedScene mainMenuScene;

    public void SetPlayerNames(string[] names)
    {
        for(int i = 0; i < names.Length; i++)
        {
            labels[i].Text = names[i];
        }
    }

    public void GoBackToMainManu()
    {
        GetTree().ChangeSceneToPacked(mainMenuScene);
    }
}
