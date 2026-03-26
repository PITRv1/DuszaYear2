using Godot;
using System;
using System.Collections.Generic;
using System.Text;

public class WinnerScreenPacket : PacketInfo
{
    public string[] names;
	public WinnerScreenPacket()
	{
		PacketType = PACKET_TYPES.WINNER_SCREEN;
	}

	public override byte[] Encode()
    {
        List<byte> data = new List<byte>();

		data.Add((byte)PacketType);

		List<byte> str = new();

		foreach (var name in names)
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

	public static WinnerScreenPacket CreateFromData(byte[] data)
	{
		WinnerScreenPacket packet = new WinnerScreenPacket();

		int index = 1;

		StringBuilder sb = new();

        var nameList = new List<string>();

		while (index < data.Length)
		{
			while (data[index] != 0)
			{
				sb.Append((char)data[index++]);
			}
			nameList.Add(sb.ToString());
			index++;
			sb.Clear();
		}

		packet.names = nameList.ToArray();

		return packet;
	}
}
