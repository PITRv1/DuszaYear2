using Godot;
using System;

public partial class PullDeck : Button
{
	private ModifierCardDeck modifierCardDeck;
	private PointCardDeck pointCardDeck;

    public override void _Ready()
    {
        modifierCardDeck = new ModifierCardDeck();
        pointCardDeck = new PointCardDeck();

		modifierCardDeck.GenerateDeck();
		pointCardDeck.GenerateDeck();
		GD.Print("START");
		pointCardDeck.PrintCards();
    }

}
