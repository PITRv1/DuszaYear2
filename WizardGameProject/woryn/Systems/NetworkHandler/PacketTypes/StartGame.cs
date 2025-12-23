using Godot;
using System;
using System.Collections.Generic;

public class StartGame : PacketInfo
{
	public byte senderId;

	public StartGame()
	{
		PacketType = PACKET_TYPES.START_GAME;
	}

	public override byte[] Encode()
    {
        List<byte> data = new List<byte>();

		data.Add((byte)PacketType);

		data.Add(senderId);
		
		return data.ToArray();
    }

	public static StartGame CreateFromData(byte[] data)
	{
		StartGame packet = new StartGame();
		int index = 1;

		packet.senderId = data[index];

		return packet;
	}
}
