using Godot;
using System.Collections.Generic;

public partial class PlayPile : Node
{
    public int TotalValue { get; private set; } = 0;

    public PointCard CurrentCard { get; private set; } = new PointCard();

    private readonly List<CardInterface> modifierCards = new();

    public override void _Ready()
    {
        CurrentCard.Value = 1;
    }

    public void NextTurn()
    {
        for (int i = modifierCards.Count - 1; i >= 0; i--)
        {
            if (modifierCards[i] is not ModifierCard card)
                continue;

            card.TurnsUntilActivation--;

            if (card.TurnsUntilActivation <= 0)
            {
                card.ActivateEffect();
                modifierCards.RemoveAt(i);
            }
        }
    }

    public int GetValueAndReset()
    {
        int value = TotalValue;
        TotalValue = 0;
        return value;
    }

    public bool AddCard(PointCard card)
    {
        if (CurrentCard.Value > card.Value)
            return false;

        CurrentCard = card;
        TotalValue += card.Value;

        
        for (int i = card.Modifiers.Count - 1; i >= 0; i--)
        {
            if (card.Modifiers[i] is not ModifierCard modifier)
                continue;

            modifierCards.Add(modifier);
            modifier.ApplyDeckModifier(this);
            card.Modifiers.RemoveAt(i);
        }

        return true;
    }
}
