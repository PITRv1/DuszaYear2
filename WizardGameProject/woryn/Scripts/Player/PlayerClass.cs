using Godot;
using System;
using System.Collections.Generic;
public partial class PlayerClass : Node
{
    public List<PointCard> PointCardList { get; }
    public List<ModifierCard> ModifCardList { get; }
    public PlayerClassInterface ChoosenClass { get; }
    public string EffectStatus {get;}
    public void DecreaseCooldown()
    {
        ChoosenClass.ActiveCooldown--;
        ChoosenClass.PassiveCooldown--;
    }
    public bool PullCardFromDeck(DeckInterface deck)
    {
        // if (deck is PointCardDeck)
        // {
        //     if (PointCardList.Count == 4)
        //         return false;
        //     else
        //         PointCardList.Add(deck.Drawcard() as PointCard);
        // }
        // else if (deck is ModifierCardDeck)
        // {
        //     if (ModifCardList.Count==4)
        //         return false;
        //     else
        //         ModifCardList.Add(deck.Drawcard() as ModifierCard);
        // }
        return true;
    }
    public bool AddModifierCard(PointCard pointCard, ModifierCard modifCard)
    {
        bool result = pointCard.AddModifier(modifCard);
        
        if (result)
            ModifCardList.Remove(modifCard);
        
        return result;
    }
    public void RemoveModifierCard(PointCard pointCard, ModifierCard modifCard)
    {
        pointCard.RemoveModifier(modifCard);
    }
    public void PlayCard(PointCard card, PlayPile playpile)
    {
        PointCardList.Remove(card);
        playpile.AddCard(card);
    }
}