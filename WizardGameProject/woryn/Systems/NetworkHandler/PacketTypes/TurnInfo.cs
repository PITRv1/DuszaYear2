using Godot;
using System;
using System.Collections.Generic;

public partial class TurnInfoPacket : PacketInfo
{
	public int PlayerId;
	public int PointCardValue;
	public List<ModifierCard> ModifierCards;
	
	public TurnInfoPacket()
	{
		PacketType = PACKET_TYPES.TURN_INFO;
		ModifierCards = new List<ModifierCard>();
	}

	public new byte[] Encode()
    {
        List<byte> data = new List<byte>();

		data.Add((byte)PacketType);
		data.AddRange(BitConverter.GetBytes(PlayerId));

		data.AddRange(BitConverter.GetBytes(PointCardValue));

		data.Add((byte)ModifierCards.Count);
		foreach (ModifierCard card in ModifierCards)
		{
			data.Add((byte)ModifierCardTypeConverter.ClassToType(card));
		}

		return data.ToArray();
    }

	public static TurnInfoPacket CreateFromData(byte[] data)
	{
		TurnInfoPacket packet = new TurnInfoPacket();
		int index = 1;

		packet.PlayerId = BitConverter.ToInt32(data, index);
		index += 4;

		packet.PointCardValue = BitConverter.ToInt32(data, index);
		index += 4;

		int modifierCount = data[index];
		index += 1;

		packet.ModifierCards = new List<ModifierCard>();
		for (int i = 0; i < modifierCount; i++)
		{
			MODIFIER_TYPES modifierType = (MODIFIER_TYPES)data[index];
			index += 4;
			packet.ModifierCards.Add(ModifierCardTypeConverter.TypeToClass(modifierType));
		}

		return packet;
	}


}
