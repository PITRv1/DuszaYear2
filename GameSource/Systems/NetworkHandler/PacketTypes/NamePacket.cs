using Godot;
using System;
using System.Collections.Generic;
using System.Text;

public class NamePacket : PacketInfo
{
    public string Name;
	public NamePacket()
	{
		PacketType = PACKET_TYPES.NAME;
	}

	public override byte[] Encode()
    {
        List<byte> data = new List<byte>();

		data.Add((byte)PacketType);

        List<byte> str = new();

        foreach (char ch in Name)
        {
            str.Add((byte)ch);
        }
        str.Add(0);

		data.AddRange(str);

		return data.ToArray();
    }

	public static NamePacket CreateFromData(byte[] data)
	{
		var packet = new NamePacket();
		var index = 1;

        StringBuilder sb = new();

        while (data[index] != 0)
        {
            sb.Append((char)data[index++]);
        }
		packet.Name = sb.ToString();

		return packet;
	}
}
