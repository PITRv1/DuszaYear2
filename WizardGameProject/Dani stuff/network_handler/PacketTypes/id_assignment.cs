using Godot;
using System.Collections.Generic;

public partial class IDAssignment : PacketInfo
{
    public int Id;
    public List<int> RemoteIds = new();

    // static create(_id, _remote_ids)
    public static IDAssignment Create(int id, List<int> remoteIds)
    {
        IDAssignment info = new IDAssignment();

        info.PacketType = PACKET_TYPE.ID_ASSIGNMENT;
        info.Flag = ENetPacketPeer.FlagReliable;
        info.Id = id;
        info.RemoteIds = remoteIds;

        return info;
    }

    // static create_from_data(data)
    public static IDAssignment CreateFromData(byte[] data)
    {
        IDAssignment info = new IDAssignment();
        info.Decode(data);
        return info;
    }

    // encode()
    public new byte[] Encode()
    {
        byte[] baseData = base.Encode();

        // 1 byte: packet type
        // 1 byte: id
        // N bytes: remote ids
        byte[] data = new byte[2 + RemoteIds.Count];

        // copy packet type from base
        data[0] = baseData[0];

        data[1] = (byte)Id;

        for (int i = 0; i < RemoteIds.Count; i++)
        {
            data[2 + i] = (byte)RemoteIds[i];
        }

        return data;
    }

    // decode(data)
    public new void Decode(byte[] data)
    {
        base.Decode(data);

        Id = data[1];
        RemoteIds.Clear();

        for (int i = 2; i < data.Length; i++)
        {
            RemoteIds.Add(data[i]);
        }
    }
}
