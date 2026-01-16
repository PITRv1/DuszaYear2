using Godot;
using System;

public partial class Multiplayer : Control
{
	[Export] CombinedUI combinedUI;
	[Export] Control multiTypeMenu;
	[Export] Control multiHostMenu;
	[Export] Control multiJoinMenu;
	[Export] Control playerListMenu;
	[Export] Control playerList;
	[Export] LineEdit gameNameText;
	[Export] HSlider numberOfPlayersValue;
    [Export] ButtonGroup optionGroup;


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
        CurrentMenu = multiTypeMenu;

		Global.multiplayerClientGlobals.HandleLocalIdAssignment += Local;
        Global.multiplayerClientGlobals.HandleRemoteIdAssignment += Remote;

		foreach (Control child in playerList.GetChildren())
		{
			playerList.RemoveChild(child);
		}
    }

	public void ChangeMenu(string option)
	{
		switch(option)
		{
			case "host":
				CurrentMenu = multiHostMenu;
				break;
			case "join":
				CurrentMenu = multiJoinMenu;
				break;
			case "select":
				CurrentMenu = multiTypeMenu;
				break;
			case "main":
				combinedUI.CurrentMenu = combinedUI.mainMenu;
				CurrentMenu = multiTypeMenu;
				break;
			case "player":
				CurrentMenu = playerListMenu;
				break;
			case "select&stop":
				ChangeMenu("select");

				Global.networkHandler.StopServer();
				Global.networkHandler.DisconnectClient();

				break;
		}
	}

	public void HostGame()
	{
        // BaseButton selected = optionGroup.GetPressedButton();

		Global.networkHandler.StartServer();
        Global.networkHandler.StartClient();

		ChangeMenu("player");
	}

	public void Local(int id)
	{
		AddPlayer(id);
	}

	public void Remote(int id)
	{
		AddPlayer(id);
	}

	public void JoinGame()
	{
        Global.networkHandler.StartClient();
		ChangeMenu("player");
	}

	private void AddPlayer(int id)
	{
		Label playerLabel = new();
		playerLabel.AddThemeFontSizeOverride("font_size", 60);
		playerLabel.Text = $"Player:{id}";
		playerList.AddChild(playerLabel);
	}
	
}
