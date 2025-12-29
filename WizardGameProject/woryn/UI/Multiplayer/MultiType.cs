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
	// If you use an automatic linter/formatter, you can avoid spare lines and invalid tabs,
	// or other badly formatted code parts, that I can see all over this file.
    [Export] ButtonGroup optionGroup;

    string gameName;
	byte numberOfPlayers;
	string gameType;
	public override void _Ready()
	{
	}


	public override void _Process(double delta)
	{
	}
	private void OnBackPressed()
	{
        hostButton.Visible = true;
        joinButton.Visible = true;
		hostMenu.Visible = false;
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
		// Try to avoid explicit var declarations. Compilers can figure it out based on the called method.
		// Like: var selected = optionGroup.GetPressedButton();
        BaseButton selected = optionGroup.GetPressedButton();

		// I would use gameName.IsNullOrEmpty check
        if (selected != null && gameName != "")
		{
            GD.Print($"Kiválasztva: {selected.Name}");
            gameType = selected.Name;
			
			GD.Print($"Game created with these parameters: Game name: {gameName} | Number of players: {numberOfPlayers} | Extra settings: --- | Game type: {gameType}");

			//Change to the playerslist scene
			// I'd outsource scene file URLs into a file that holds all of them in an enum or a static class. 
			// So if you refactor, you need to change it at one place/
			GetTree().ChangeSceneToFile("res://UI/Multiplayer/PlayerSelection.tscn");


        }
		else
		{
			// Always avoid using hungarian in codebase.
			GD.Print("Hiányzó adatok");
			return;
		}


	}
}
