using Godot;
using System;

public class CAlchemist : PlayerClassInterface
{
	public string ClassName { get; set; } = "Alchemist" ;
    public int ActiveCooldown { get; set; } = 3 ;
    public int PassiveCooldown { get; set; } = 0 ;
    public PlayerClass Parent { get; set; }
    public void UseActive()
    {
        if (Parent.chosenPointCard!=null)
            Parent.chosenPointCard.CardRarity = CardRaritiesEnum.LEGENDARY;
    }
    public void UsePassive()
    {
        //még nincs passive countdown 
    }
}