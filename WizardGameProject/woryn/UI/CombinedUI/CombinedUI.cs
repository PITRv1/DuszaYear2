using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class CombinedUI : Control
{
	[Signal] public delegate void MenuChangedEventHandler(Control newMenu);

	[Export] public Control mainMenu;
	[Export] public Control creditsMenu;
	[Export] public Control settingsMenu;
	[Export] public Control singpePlayerMenu;
	[Export] public Control multiplayerCombinedMenu;

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
			EmitSignal(SignalName.MenuChanged, _currentMenu);
		}
	}

    public override void _Ready()
    {
        CurrentMenu = mainMenu;
    }


}
