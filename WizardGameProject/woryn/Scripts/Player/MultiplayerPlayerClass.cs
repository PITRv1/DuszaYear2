using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class MultiplayerPlayerClass : Node
{
    public PlayerClass playerClass;
    public int ID;
    List<int> ids = new();

    [Export] HBoxContainer pointCards;
    [Export] HBoxContainer modifCards;
    [Export] PackedScene pointCardUI;
    [Export] PackedScene modifierCardUI;
    [Export] Label maxPoints;
    
    public override void _Ready()
    {
        Global.multiplayerClientGlobals.HandleLocalIdAssignment += Local;
        Global.multiplayerClientGlobals.HandleRemoteIdAssignment += Remote;
        Global.networkHandler.OnPeerConnected += Remote;

        Global.multiplayerClientGlobals.HandleTurnInfo += playerClass.ProccessTurnInfoPacket;
        Global.multiplayerClientGlobals.HandlePickUpCardAnswer += playerClass.ProccessPickUpAnswer;
    }

    public MultiplayerPlayerClass()
    {
        playerClass = new PlayerClass();
        playerClass.parent = this;
    }

    private void Local(int id)
    {
        ID = id;
    }

    private void Remote(int id)
    {
        ids.Add(id);
    }

    public void SetMaxPoints(int points)
    {
        maxPoints.Text = points.ToString();
    }

    public void PlayCard()
    {
        if (playerClass.CanEndTurn())
            return;

        List<byte> modifIndexes = new List<byte>();

        foreach (ModifierCard card in playerClass.chosenModifierCards)
        {
            modifIndexes.Add((byte)playerClass.ModifCardList.IndexOf(card));
        }

        EndTurnRequest packet = new EndTurnRequest
        {
            SenderId = ID,
            PointCard = playerClass.chosenPointCard,
            PointCardIndex = playerClass.PointCardList.IndexOf(playerClass.chosenPointCard),
            ModifierCards = playerClass.chosenModifierCards.ToArray(),
            ModifCardIndexes = modifIndexes.ToArray()
        };

        Global.networkHandler._serverPeer?.Send(0, packet.Encode(), (int)ENetPacketPeer.FlagReliable);
    }

    public void RemoveSelectedCards(int lastPlayer)
    {
        if (lastPlayer != ID)
            return;
        pointCards.RemoveChild(pointCards.GetChild(playerClass.PointCardList.IndexOf(playerClass.chosenPointCard)));
        playerClass.PointCardList.Remove(playerClass.chosenPointCard);
        
        foreach (ModifierCard card in playerClass.chosenModifierCards)
        {
            playerClass.ModifCardList.Remove(card);
        }

        List<byte> modifIndexes = new List<byte>();

        foreach (ModifierCard card in playerClass.chosenModifierCards)
        {
            modifIndexes.Add((byte)playerClass.ModifCardList.IndexOf(card));
        }

        modifIndexes.Sort();
        modifIndexes.Reverse();

        foreach (int index in modifIndexes)
        {
            modifCards.RemoveChild(pointCards.GetChild(index));
        }
    }

    public void PickUpCards()
    {
        PickUpCardRequest packet = new PickUpCardRequest
        {
            SenderId = ID,
        };

        Global.networkHandler._serverPeer?.Send(0, packet.Encode(), (int)ENetPacketPeer.FlagReliable);
    }

    public void AddPointToContainer(PointCard pointCard)
    {
        TestPointCardUi test = pointCardUI.Instantiate() as TestPointCardUi;
        test.text.Text = pointCard.PointValue.ToString();
        test.pointCard = pointCard;
        test.playerClass = playerClass;
        pointCards.AddChild(test);
    }

    public void AddModifierToContainer(ModifierCard card)
    {
        TestModifierCardUi test = modifierCardUI.Instantiate() as TestModifierCardUi;

        switch (card.ModifierType)
        {
            case MODIFIER_TYPES.MULTIPLIER:
                test.text.Text = $"{(card as ModifierCardMultiplier).Amount}";
                test.modifierCard = card;
                test.playerClass = playerClass;
                break;
        }

        modifCards.AddChild(test);
    }
}
