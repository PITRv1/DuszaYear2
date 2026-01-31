using Godot;
using System;

public partial class PublicShop : Control
{
    [Export] Timer publicShopTimer;
    [Export] Label publicShopTimerLabel;
    public override void _Process(double delta)
    {
        publicShopTimerLabel.Text = Math.Round(publicShopTimer.TimeLeft).ToString();
    }
    public void OnTimerTimeout()
    {
        GD.Print("Public shop timer ended");
    }


    public void OnNextButtonPressed()
    {
        GD.Print("Go to the private shop!");
    }
}
