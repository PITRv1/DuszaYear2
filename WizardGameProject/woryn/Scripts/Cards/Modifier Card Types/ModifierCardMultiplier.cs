using Godot;
using System;

public partial class ModifierCardMultiplier : ModifierCard
{
	public MODIFIER_TYPES ModifierType => MODIFIER_TYPES.MULTIPLIER;
	public int Amount { get; private set; } = 2;

    public string CardName { get; } = "Faszom tudja";

    public bool IsCardModifier { get; } = true;

    public int TurnsUntilActivation => 0;

    public void ActivateEffect()
    {
        throw new NotImplementedException();
    }

    public void ApplyDeckModifier(PlayPile playPile)
    {
        throw new NotImplementedException();
    }

	public void RandomizeProperties()
	{
		RandomNumberGenerator rng = new RandomNumberGenerator();
		Amount = rng.RandiRange(2, 6);
	}

	public int Calculate(int value)
	{
		return value * Amount;
	}

    public byte PacketValue()
    {
        throw new NotImplementedException();
    }

}
