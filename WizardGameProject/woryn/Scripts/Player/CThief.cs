using Godot;
using System;

public class CThief : PlayerClassInterface
{
	public string ClassName { get; set; } = "Thief" ;
    public int ActiveCooldown { get; set; } = 3 ;
    public int PassiveCooldown { get; set; } = 0 ;
    public PlayerClass Parent { get; set; }
    public void UseActive()
    {
        
    }
    public void UsePassive()
    {
        //még nincs passive countdown 
    }
}