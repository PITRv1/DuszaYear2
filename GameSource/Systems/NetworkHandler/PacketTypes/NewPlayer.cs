using Godot;
using System;
using System.Collections.Generic;
using System.Text;

public class NewPlayer : PacketInfo
{
	public string[] playerArray;

	public NewPlayer()
	{
		PacketType = PACKET_TYPES.NEW_PLAYER;
	}

	public override byte[] Encode()
    {
        List<byte> data = new List<byte>();

		data.Add((byte)PacketType);

		data.Add((byte)playerArray.Length);
		
		List<byte> str = new();

		foreach (string name in playerArray)
		{
			foreach (char ch in name)
			{
				str.Add((byte)ch);
			}
			str.Add(0);

			data.AddRange(str);

			str.Clear();
		}

		return data.ToArray();
    }

	public static NewPlayer CreateFromData(byte[] data)
	{
		NewPlayer packet = new NewPlayer();
		int index = 1;

		StringBuilder sb = new();
		List<string> names = new();

		while (index < data.Length)
		{
			while (data[index] != 0)
			{
				sb.Append((char)data[index++]);
			}
			names.Add(sb.ToString());
			sb.Clear();
		}
        
		packet.playerArray = names.ToArray();

		return packet;
	}
}
