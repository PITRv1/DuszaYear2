using Godot;
using System;


public static class ModifierCardTypeConverter
{

	public static MODIFIER_TYPES ClassToType(ModifierCard card)
	{
		return card switch
		{
			ModifierCardMultiplier => MODIFIER_TYPES.MULTIPLIER,
			_ => MODIFIER_TYPES.NONE
		};
	}

	public static ModifierCard TypeToClass(MODIFIER_TYPES card)
	{
		return card switch
		{
			MODIFIER_TYPES.MULTIPLIER => new ModifierCardMultiplier(),
			_ => null
		};
	}

}

public interface ModifierCard
{
	public string CardName { get; }
	public bool IsCardModifier { get; }
	public int TurnsUntilActivation { get; set; }
	public MODIFIER_TYPES ModifierType { get; }
	public void ApplyDeckModifier(PlayPile playPile);
	public void ActivateEffect();
	public int Calculate(int value);
	public byte PacketValue();
}
