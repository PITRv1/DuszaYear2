using Godot;
using System;

public partial class MultiType : VBoxContainer
{
	[Export] PackedScene gameScene;
	[Export] Button hostButton;
	[Export] Button joinButton;
	[Export] VBoxContainer hostMenu;
	[Export] PackedScene playersListScene;

	//Parameter variables
	[Export] TextEdit gameNameText;
	[Export] HSlider numberOfPlayersValue;
    [Export] ButtonGroup optionGroup;

    string gameName;
	byte numberOfPlayers;
	string gameType;

	private void OnJoinPressed()
	{
		//Joining to the game
		//GetTree().ChangeSceneToPacked(gameScene);
		GD.Print("Joining...");
	}

    private void OnHostPressed()
    {
		hostButton.Visible = false;
		joinButton.Visible = false;
		hostMenu.Visible = true;
		Visible = false;
        GD.Print("Hosting...");
    }

	private void OnCreatePressed()
	{
		gameName = gameNameText.Text;
		numberOfPlayers = Convert.ToByte(numberOfPlayersValue.Value);
        BaseButton selected = optionGroup.GetPressedButton();

        if (selected == null && gameName == "") return;
		
		gameType = selected.Name;
		GetTree().ChangeSceneToFile("res://UI/Multiplayer/PlayerSelection.tscn");
	}
}
