using Godot;
using System.Collections.Generic;

public partial class ModifierCardDeck : Node
{
    List<ModifierCard> modifierCards;

    [Export]
    public int Amount { get; set; } = 28;

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
}
