using Godot;
using System;
using System.Collections.Generic;

public enum PASSIVEICONS {
	DICE,
	MIRROR,
	MONEY
}

public partial class PlayerHud : Control
{
	[Export] Label timerLabel;
	[Export] Timer timer;
	[Export] RichTextLabel goldLabel;
	[Export] RichTextLabel pointLabel;
	[Export] TextureRect passiveTextureRect;
	[Export] Godot.Collections.Array<Texture2D> passiveTextures;
	[Export] AnimationPlayer animationPlayer;

	List<PASSIVEICONS> passiveIconBuffer = new();

	public void ShowPassive(PASSIVEICONS icon)
	{
		passiveIconBuffer.Add(icon);
	}

    public override void _Ready()
    {
        Global.multiplayerClientGlobals.PassiveUsed += (byte[] data) =>
		{
			var packet = PassiveUsed.CreateFromData(data);
			ShowPassive(packet.icon);
		};
    }

	private void ShowNextPassiveIcon()
	{
		PASSIVEICONS currentIcon = passiveIconBuffer[0];
		passiveIconBuffer.RemoveAt(0);

		passiveTextureRect.Texture = passiveTextures[(int)currentIcon];
		animationPlayer.Play("ShowPassive");
	}

	public void StartCountdownTimer(double time = 30)
	{
		timer.Start(time);
	}

	public void StopCountdownTimer()
	{
		timer.Stop();
	}

	public void UpdateGoldAmount(int amount)
	{
		goldLabel.Text =$"[wave freq=1] Gold: {amount}";
	}

	public void UpdatePointsAmount(int amount)
	{
		pointLabel.Text =$"[rainbow freq=0.2][wave freq=1] Points: {amount}";
	}

    public override void _Process(double delta)
    {
        timerLabel.Text = Convert.ToInt32(timer.TimeLeft).ToString();
        if (passiveIconBuffer.Count > 0 && !animationPlayer.IsPlaying()) ShowNextPassiveIcon();
    }


}
