using Godot;
using System;

public partial class MultiType : HBoxContainer
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
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

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
        GD.Print("Hosting...");
    }

	private void OnCreatePressed()
	{
		gameName = gameNameText.Text;
		numberOfPlayers = Convert.ToByte(numberOfPlayersValue.Value);
        BaseButton selected = optionGroup.GetPressedButton();

        if (selected != null && gameName != "")
		{
            GD.Print($"Kiválasztva: {selected.Name}");
            gameType = selected.Name;
			
			GD.Print($"Game created with these parameters: Game name: {gameName} | Number of players: {numberOfPlayers} | Extra settings: --- | Game type: {gameType}");

			//Change to the playerslist scene
			GetTree().ChangeSceneToFile("res://UI/Multiplayer/PlayerSelection.tscn");


        }
		else
		{
			GD.Print("Hiányzó adatok");
			return;
		}


	}
}
