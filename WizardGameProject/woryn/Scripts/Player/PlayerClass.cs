using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public class PlayerClass
{
    public List<PointCard> PointCardList { get; }
    public List<ModifierCard> ModifCardList { get; }
    public PlayerClassInterface ChoosenClass { get; }
    public ModifierCardDeck modifierCardDeck { get; }
    public MultiplayerPlayerClass parent;
    public int Points { get; set; } = 0;

    public PointCard chosenPointCard ;
    // {
    //     get
    //     {
    //         return chosenPointCard;
    //     }
    //     set
    //     {
    //         chosenModifierCards.Clear();
    //         chosenPointCard = value;
    //     }
    // }
    public readonly List<ModifierCard> chosenModifierCards = new();

    public string EffectStatus { get; }
    
    public PlayerClass()
    {
        PointCardList = new List<PointCard>();
        ModifCardList = new List<ModifierCard>();

        modifierCardDeck = new ModifierCardDeck();
        modifierCardDeck.GenerateDeck();
    }

    public bool AddToChosenModifierCards(ModifierCard card)
    {
        if (chosenPointCard == null)
            return false;
        if (chosenModifierCards.Count >= (int)chosenPointCard.CardRarity)
            return false;
        
        chosenModifierCards.Add(card);
        return true;
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
        Points = packet.CurrentPointValue;
        parent.SetUI(packet.MaxValue, packet.CurrentPointValue, packet.ThrowDeckValue);
        parent.RemoveSelectedCards(packet.LastPlayer);
    }

    public void ProccessPickUpAnswer(byte[] data)
    {
        GD.Print("why");
        PickUpCardAnswer packet = PickUpCardAnswer.CreateFromData(data);
        // if (packet.PointCards.Length == 0) return;

        PointCardList.AddRange(packet.PointCards);
        ModifCardList.AddRange(packet.ModifierCards);

        foreach (PointCard card in packet.PointCards)
        {
            parent.AddPointToContainer(card);
        }

        foreach (ModifierCard card in packet.ModifierCards)
        {
            GD.Print("MEDEWDEDEDVE");
            parent.AddModifierToContainer(card);
        }

        GD.Print("BRIHER");
    }

    public bool CanEndTurn()
    {
        return chosenPointCard == null;
    }
}