using Godot;
using System.Collections.Generic;
using System.ComponentModel;

public partial class PointCardDeck : Node
{
    List<PointCard> pointCards;

    // private int MaxNumber = 9; For later use
    private int NumberOfCards = 36;
    
    public PointCardDeck()
    {
        pointCards = new List<PointCard>();
    }

    public void GenerateDeck()
    {
        pointCards.Clear();

        for (int i = 0; i < NumberOfCards; i++)
        {
            pointCards.Add(new PointCard(i / 4 + 1));
        }

        ShuffleCards();
    }

    // Medve made this, I hope it works
    private void ShuffleCards()
    {
        var rng = new RandomNumberGenerator(); 
        rng.Randomize();

        for (int i = pointCards.Count - 1; i > 0; i--)
        {
            int j = rng.RandiRange(0, i);
            (pointCards[i], pointCards[j]) = (pointCards[j], pointCards[i]);
        }
    }

    public void PrintCards()
    {
        string points = "";
        int buh = 0;
        foreach (PointCard pointCard in pointCards)
        {
            points += pointCard.PointValue + " ";
            buh++;
        }
        GD.Print(points + " " + buh);
    }
}
