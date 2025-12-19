using Godot;
using System.Collections.Generic;

public partial class MultiplayerClientGlobals : Node
{
    /* ================= SIGNALS ================= */

    [Signal]
    public delegate void HandleLocalIdAssignmentEventHandler(int localId);

    [Signal]
    public delegate void HandleRemoteIdAssignmentEventHandler(int remoteId);


    /* ================= STATE ================= */

    private int _id = -1;
    private List<int> _remoteIds = new();

    /* ================= LIFECYCLE ================= */

    public override void _Ready()
    {
        Globals.networkHandler.OnClientPacket += OnClientPacket;
    }

    /* ================= PACKET HANDLING ================= */

    private void OnClientPacket(byte[] data)
    {
        PacketInfo.PACKET_TYPE packetType =
            (PacketInfo.PACKET_TYPE)data[0];

        switch (packetType)
        {
            case PacketInfo.PACKET_TYPE.ID_ASSIGNMENT:
                ManageIds(IDAssignment.CreateFromData(data));
                break;

            case PacketInfo.PACKET_TYPE.PLAYER_POSITION:
                GD.PushError($"Player position is unhandled due to dani kukáing it.");
                break;

            default:
                GD.PushError($"Packet type with index {(int)packetType} unhandled!");
                break;
        }
    }

    /* ================= ID MANAGEMENT ================= */

    private void ManageIds(IDAssignment idAssignment)
    {
        // First assignment (local client ID)
        if (_id == -1)
        {
            _id = idAssignment.Id;
            EmitSignal(SignalName.HandleLocalIdAssignment, _id);

            _remoteIds = new List<int>(idAssignment.RemoteIds);

            foreach (int remoteId in _remoteIds)
            {
                if (remoteId == _id)
                    continue;

                EmitSignal(SignalName.HandleRemoteIdAssignment, remoteId);
            }
        }
        // Subsequent assignments (new remote peers)
        else
        {
            _remoteIds.Add(idAssignment.Id);
            EmitSignal(SignalName.HandleRemoteIdAssignment, idAssignment.Id);
        }
    }
}
