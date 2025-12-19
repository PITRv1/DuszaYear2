using Godot;
using System.Collections.Generic;

public partial class ModifierCardDeck : Node
{
    List<ModifierCard> modifierCards;

    [Export]
    public int Amount { get; set; } = 28;

    public ModifierCardDeck()
    {
        modifierCards = new List<ModifierCard>();
    }

    public void GenerateDeck()
    {
        modifierCards.Clear();

        for (int i = 0; i < Amount; i++)
        {
            ModifierCardMultiplier modifierCard = new ModifierCardMultiplier();
            modifierCard.RandomizeProperties();

            modifierCards.Add(modifierCard);
        }
    }

    public void PrintCards()
    {
        foreach (ModifierCard pointCard in modifierCards)
        {
            GD.PrintRaw(pointCard.Name + " ");
        }
    }
}
