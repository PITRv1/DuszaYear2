using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public partial class PlayerClass
{
    public List<PointCard> PointCardList { get; }
    public List<ModifierCard> ModifCardList { get; }
    public PlayerClassInterface ChoosenClass { get; }
    public MultiplayerPlayerClass parent;

    public PointCard chosenPointCard;
    public List<ModifierCard> chosenModifierCards = new();

    public string EffectStatus { get; }
    
    public PlayerClass()
    {
        PointCardList = new List<PointCard>();
        ModifCardList = new List<ModifierCard>();
    }
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

    public void ProccessTurnInfoPacket(byte[] data)
    {
        TurnInfoPacket packet = TurnInfoPacket.CreateFromData(data);
    }

    public void ProccessPickUpAnswer(byte[] data)
    {
        PickUpCardAnswer packet = PickUpCardAnswer.CreateFromData(data);

        if (packet.PointCards.Length == 0) return;

        PointCardList.AddRange(packet.PointCards);
        ModifCardList.AddRange(packet.ModifierCards);

        foreach (PointCard card in PointCardList)
        {
            parent.AddPointToContainer(card);
        }

        foreach (ModifierCard card in ModifCardList)
        {
            parent.AddModifierToContainer(card);
        }

        GD.Print("BRIHER");
    }

    public bool CanEndTurn()
    {
        return chosenPointCard == null;
    }
}