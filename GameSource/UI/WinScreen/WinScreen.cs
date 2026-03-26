using Godot;
using System;

public partial class WinScreen : Control
{
    [Export] Godot.Collections.Array<Label> labels;

    public void SetPlayerNames(string[] names)
    {
        for(int i = 0; i < names.Length; i++)
        {
            labels[i].Text = names[i];
        }
    }   
}
