using Godot;
using System;
using System.Collections.Generic;
public abstract class PlayerClass : PlayerClassInterface
{
    public string ClassName { get; private set;}
    public byte ActiveCooldown { get; private set;}
    public byte PassiveCooldown { get; private set;}
    public void UseActive()
    {
        
    }
    public void UsePassive()
    {
        
    }
    public List<PointCard> PointCardList { get; }
    public List<ModifierCard> ModifCardList { get;}
    public PlayerPlayableClassInterface ChoosenClass {get;}
    public string EffectStatus {get;}
    public void DecreaseCooldown()
    {
        ChoosenClass.ActiveCooldown--;
        ChoosenClass.PassiveCooldown--;
    }
    public bool PullCardFromDeck(DeckInterface deck)
    {
        if (deck is PointCardDeck)
        {
            if (PointCardList.Count==4){ return false; }
            else
            {
                PointCardList.Add(deck.drawcard()); 
            }
        }else if(deck is ModifierCardDeck)
        {
            if (ModifCardList.Count==4){ return false; }
            else
            {
                ModifCardList.Add(deck.drawcard());
            }
        }
        return true;
    }
    public bool AddModifierCard(PointCard pointCard, ModifierCardInterface modifCard)
    {
    var result = pointCard.AddModifier(modifCard);
	
	if (result) { ModifCardList.Remove(modifCard); }
	
	return result;
    }
    public void RemoveModifierCard(PointCard pointCard, ModifierCardInterface modifCard)
    {
        pointCard.RemoveModifier(modifCard);
    }
    public void PlayCard(PointCard card, PlayPile playpile)
    {
        PointCardList.Remove(card);
        playpile.AddCard(card);
    }
}