using Godot;
using System.Collections.Generic;

public partial class PointCardDeck : DeckInterface
{
    //[ExportRange(1, 9, 1)]
    public int CardTypes { get; set; } = 9;

    //[ExportRange(1, 9, 1)]
    public int CardsPerType { get; set; } = 3;

    public override void GenerateDeck()
    {
        Cards.Clear();

        for (int pointCardNum = 1; pointCardNum <= CardTypes; pointCardNum++)
        {
            for (int i = 0; i < CardsPerType; i++)
            {
                //PointCard pointCard = new PointCard{PointValue = pointCardNum}; a set ugye privát szóval nem lehet beállítani szoo ikd

                pointCard.RandomizeRarity();
                Cards.Add(pointCard);
            }
        }

        ShuffleCards();
    }

    private void ShuffleCards()
    {
        var rng = new RandomNumberGenerator();
        rng.Randomize();

        for (int i = Cards.Count - 1; i > 0; i--)
        {
            int j = rng.RandiRange(0, i);
            (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
        }
    }
}
