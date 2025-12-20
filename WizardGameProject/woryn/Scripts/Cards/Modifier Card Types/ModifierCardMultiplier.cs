using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class ModifierCardMultiplier : ModifierCard
{
	public int Amount { get; private set; } = 2;
	public void RandomizeProperties()
	{
		RandomNumberGenerator rng = new RandomNumberGenerator();
		Amount = rng.RandiRange(2, 6);
	}
}
