using Godot;

public partial class ModifierCardDeck : DeckInterface
{
    [Export]
    public int Amount { get; set; } = 28;

    public override void GenerateDeck()
    {
        Cards.Clear();

        for (int i = 0; i < Amount; i++)
        {
            ModifierCardMultiplier modifierCard = new ModifierCardMultiplier();
            modifierCard.RandomizeProperties();

            Cards.Add(modifierCard);
        }
    }
}
