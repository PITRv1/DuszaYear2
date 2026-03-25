using Godot;
using System;
using System.Collections;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

public partial class Settings : Control
{
	[Export] MainUI _mainUI;
	[Export] HSlider _masterVolumeSlider;
	[Export] HSlider _musicVolumeSlider;
	[Export] HSlider _sfxVolume;
	[Export] CheckBox _shaders;
	[Export] LineEdit _name;
	public const string SAVE_FILE = "res://Saves/Save.json";
	private static JsonSerializerOptions options = new JsonSerializerOptions
	{
		WriteIndented = true,
	};

	// [Export] Label masterVolumeLabel;
	// [Export] Label musicVolumeLabel;

    public override void _Ready()
    {
		Load();
        _masterVolumeSlider.Value = AudioServer.GetBusVolumeDb(0);
        _musicVolumeSlider.Value = AudioServer.GetBusVolumeDb(1);
    }

	public bool SaveExists()
	{
		var path = ProjectSettings.GlobalizePath(SAVE_FILE);
		return File.Exists(path);
	}

	public void MasterVolumeChanged(float value)
	{
		GD.Print("CHANGED!");
		AudioManager.SetMasterVolume(value);
		Save();
        // masterVolumeLabel.Text = (masterVolumeSlider.Value / masterVolumeSlider.MaxValue * 100).ToString();
	}

	public void MusicVolumeChanged(float value)
	{
		GD.Print("CHANGED!");
		AudioManager.SetMusicVolume(value);
		Save();
        // musicVolumeLabel.Text = (musicVolumeSlider.Value / musicVolumeSlider.MaxValue * 100).ToString();
	}

	public void SFXVolumeChanged(float value)
	{
		GD.Print("CHANGED!");
		Save();
        // musicVolumeLabel.Text = (musicVolumeSlider.Value / musicVolumeSlider.MaxValue * 100).ToString();
	}

	public void ShadersToggled(bool value)
	{
		GD.Print("CHANGED!");
		Save();
	}

	public void NameChanged(string value)
	{
		GD.Print("CHANGED!");
		Save();
		Global.multiplayerClientGlobals.ClientName = value;
	}

	public void Back()
	{
		_mainUI.ResetToMainMenu();
	}

	public void Save()
	{
		var save_data = new SettingsData
		{
			MainVolume 	= _masterVolumeSlider.Value,
    		MusicVolume = _musicVolumeSlider.Value,
    		SFXVolume 	= _sfxVolume.Value,
    		Shaders 	= _shaders.Disabled,
    		Name 		= _name.Text,
		};
		var contents = JsonSerializer.Serialize<SettingsData>(save_data, options);
		File.WriteAllText(ProjectSettings.GlobalizePath(SAVE_FILE), contents);
		
	}
	public void Load()
	{
		if (!SaveExists())
			return;
		string data = File.ReadAllText(ProjectSettings.GlobalizePath(SAVE_FILE));
		var save_data = JsonSerializer.Deserialize<SettingsData>(data);
		_masterVolumeSlider.Value = save_data.MainVolume;
		_musicVolumeSlider.Value = save_data.MusicVolume;
		_sfxVolume.Value = save_data.SFXVolume;
		_shaders.Disabled = save_data.Shaders;
		_name.Text = save_data.Name;
	}

	public void SetViewportSize(int option)
	{
		switch (option)
		{
			case 0:
				EditAndSaveViewportSize(new Vector2I(640, 480));
				break;
			case 1:
				EditAndSaveViewportSize(new Vector2I(1280, 720));
				break;
			case 2:
				EditAndSaveViewportSize(new Vector2I(1920, 1080));
				break;
			case 3:
				EditAndSaveViewportSize(new Vector2I(2560, 1440));
				break;
			case 4:
				EditAndSaveViewportSize(new Vector2I(3480, 2160));
				break;
		}
	}

	public void SetWindowMode(int option)
	{
		switch (option)
		{
			case 0:
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
				break;
			case 1:
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
				break;
			case 2:
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Maximized);
				break;
		}
	}

	private void EditAndSaveViewportSize(Vector2I size)
	{
		GetViewport().Set("size", size);
	}
}
