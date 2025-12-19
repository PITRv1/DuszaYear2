using Godot;
using System;
using System.Linq;

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
	}

    public override void _Pressed()
    {
		GD.Print("Pressed");
		int count = 4 < pointCardDeck.GetCount() ? 4 : pointCardDeck.GetCount();
		// ModifierCard[] modifierCards = modifierCardDeck.PullCards(4);
		PointCard[] pointCards = pointCardDeck.PullCards(count);

		foreach (PointCard pointCard in pointCards)
		{
			GD.Print(pointCard.PointValue);
		}
		pointCardDeck.PrintCards();
    }  


}
