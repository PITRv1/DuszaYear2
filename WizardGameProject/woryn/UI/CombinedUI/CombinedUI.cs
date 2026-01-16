using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class CombinedUI : Control
{
	[Export] public Control mainMenu;
	[Export] public Control creditsMenu;
	[Export] public Control settingsMenu;
	[Export] public Control singpePlayerMenu;
	[Export] public Control multiplayerCombinedMenu;

	[Export] public MainMenuController mainMenuController;

	private Control _currentMenu;

	public Control CurrentMenu {
		get
		{
			return _currentMenu;
		}
		set {
			if (_currentMenu != null) _currentMenu.Visible = false;
			_currentMenu = value;
			_currentMenu.Visible = true;
		}
	}

    public override void _Ready()
    {
        CurrentMenu = mainMenu;
    }


}
