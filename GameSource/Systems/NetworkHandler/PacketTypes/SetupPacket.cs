using Godot;
using System;
using System.Collections.Generic;
using System.Text;

public partial class SetupPacket : PacketInfo
{
	public int PlayerCount;
	public int StarterPlayer;
	public Dictionary<byte, string> players = new();
	public SetupPacket()
	{
		PacketType = PACKET_TYPES.SETUP_PLACE;
	}

	public override byte[] Encode()
    {
        List<byte> data = new List<byte>();

		data.Add((byte)PacketType);

		data.AddRange(BitConverter.GetBytes(PlayerCount));

		data.AddRange(BitConverter.GetBytes(StarterPlayer));

		List<byte> str = new();

		foreach (var key in players.Keys)
		{
			data.Add(key);

			GD.Print("name bruh: " + players[key]);

			foreach (char ch in players[key])
			{
				str.Add((byte)ch);
			}
			str.Add(0);

			data.AddRange(str);
			str.Clear();
		}

		return data.ToArray();
    }

	public static SetupPacket CreateFromData(byte[] data)
	{
		SetupPacket packet = new SetupPacket();

		int index = 1;

		packet.PlayerCount = BitConverter.ToInt32(data, index);
		index += 4;

		packet.StarterPlayer = BitConverter.ToInt32(data, index);
		index += 4;

		StringBuilder sb = new();

		packet.players = new();

		while (index < data.Length)
		{
			byte id = data[index++];

			while (data[index] != 0)
			{
				sb.Append((char)data[index++]);
			}
			packet.players.Add(id, sb.ToString());
			index++;
			sb.Clear();
		}


		return packet;
	}
}
