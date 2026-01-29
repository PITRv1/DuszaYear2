using Godot;
using System;

public partial class MainMenu : Control
{
	[Export] CombinedUI combinedUI;

	[Export] Control quitPopUp;

	public void MenuSelected(string menuName)
	{
		switch (menuName)
		{
			case "credits":
				combinedUI.CurrentMenu = combinedUI.creditsMenu;
				break;
			case "settings":
				combinedUI.CurrentMenu = combinedUI.settingsMenu;

				break;
			case "single":
				combinedUI.CurrentMenu = combinedUI.singpePlayerMenu;

				break;
			case "multi":
				combinedUI.CurrentMenu = combinedUI.multiplayerCombinedMenu;

				break;
			case "quit":
				quitPopUp.Visible = !quitPopUp.Visible;
				break;
		}
	}

	public void Quit()
	{
		GetTree().Quit();
	}
}
