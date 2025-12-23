using Godot;
using System;

public partial class PlayerFein : Panel
{
	[Export] Label text;

	public void SetText(string newText)
	{
		text.Text = newText;
	}
}
