using Godot;
using System;

public class CThief : PlayerClassInterface
{
	public string ClassName { get; set; } = "Thief" ;
    public int ActiveCooldown { get; set; } = 32;
    public int PassiveCooldown { get; set; } = 0 ;
    public PlayerClass Parent { get; set; }
    public void UseActive()
    {
        //steal points
        //choose player here (UI)
        PlayerClass choosenPlayer;
        
        parent.points+= choosenPlayer.Points;
        choosenPlayer.points = 0;
    }
    public void UsePassive()
    {
        // Legyen megmutatva az első 3 káryta a pakliból a játékosnak és válasszon (UI)
        PointCard choosenPCard;
        ModifierCard choosenMCard;
        
        
    }
}