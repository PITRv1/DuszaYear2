using Godot;
using System;

public partial class Singleplayer : Control
{
	[Export] CombinedUI combinedUI;

	public void Back()
	{
		combinedUI.CurrentMenu = combinedUI.mainMenu;
		combinedUI.mainMenuController.GoToMainViewPoint();

	}
}
