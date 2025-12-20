using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public enum ModifierCardType
{
	NONE,
	MULTIPLIER,
}

public static class ModifierCardTypeConverter
{
	public static ModifierCardType ClassToType(ModifierCard card)
	{
		return card switch
		{
			ModifierCardMultiplier => ModifierCardType.MULTIPLIER,
			_ => ModifierCardType.NONE
		};
	}
}

public interface ModifierCard
{
	public string CardName { get; }
	public bool IsCardModifier { get; }
	public int TurnsUntilActivation { get; }
	public void ApplyDeckModifier(PlayPile playPile);
	public void ActivateEffect();
	public int Calculate(int value);
}
