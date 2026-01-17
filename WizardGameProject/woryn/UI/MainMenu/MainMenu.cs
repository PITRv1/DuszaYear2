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
				combinedUI.mainMenuController.GoToShelfViewPoint();
				break;
			case "settings":
				combinedUI.CurrentMenu = combinedUI.settingsMenu;
				combinedUI.mainMenuController.GoToBarViewPoint();

				break;
			case "single":
				combinedUI.CurrentMenu = combinedUI.singpePlayerMenu;
				combinedUI.mainMenuController.GoToPlayViewPoint();

				break;
			case "multi":
				combinedUI.CurrentMenu = combinedUI.multiplayerCombinedMenu;
				combinedUI.mainMenuController.GoToPlayViewPoint();

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
