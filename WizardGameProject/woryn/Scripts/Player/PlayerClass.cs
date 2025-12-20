using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public partial class PlayerClass : Node
{
    public List<PointCard> PointCardList { get; }
    public List<ModifierCard> ModifCardList { get; }
    public PlayerClassInterface ChoosenClass { get; }
    public string EffectStatus { get; }
    public void DecreaseCooldown()
    {
        ChoosenClass.ActiveCooldown--;
        ChoosenClass.PassiveCooldown--;
    }

    public void AddCardToPointCards(PointCard card)
    {
        if (PointCardList.Count == 4)
            return;
        PointCardList.Append(card);
    }

    public void AddCardToModifierCards(ModifierCard card)
    {
        if (ModifCardList.Count == 4)
            return;
        ModifCardList.Append(card);
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