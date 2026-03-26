using Godot;
using System;
using System.Collections.Generic;

public class PassiveUsed : PacketInfo
{
    public PASSIVEICONS icon;
	public PassiveUsed()
    {
        PacketType = PACKET_TYPES.PASSIVE_USED;
    }
	
	public override byte[] Encode()
	{
		var data = new List<byte>();

		data.Add((byte)PacketType);

        data.AddRange(BitConverter.GetBytes((int)icon));

		return data.ToArray();
	}
	
	public static PassiveUsed CreateFromData(byte[] data)
	{
		var packet = new PassiveUsed();
		var index = 1;

        packet.icon = (PASSIVEICONS)BitConverter.ToInt32(data, index);        
		return packet;
	}
}
