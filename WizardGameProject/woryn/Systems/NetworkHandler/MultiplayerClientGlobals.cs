using Godot;
using System.Collections.Generic;

public partial class MultiplayerClientGlobals : Node
{
    public MultiplayerClientGlobals()
    {
        Global.multiplayerClientGlobals = this;
    }

    [Signal]
    public delegate void HandleLocalIdAssignmentEventHandler(int localId);

    [Signal]
    public delegate void HandleRemoteIdAssignmentEventHandler(int remoteId);

    private int _id = -1;
    private List<int> _remoteIds = new();

    public override void _Ready()
    {
        Global.networkHandler.OnClientPacket += OnClientPacket;
    }

    private void OnClientPacket(byte[] data)
    {
        PACKET_TYPES packetType =(PACKET_TYPES)data[0];

        switch (packetType)
        {
            case PACKET_TYPES.ID_ASSIGNMENT:
                ManageIds(IDAssignment.CreateFromData(data));
                break;
            case PACKET_TYPES.TURN_INFO:
                break;
            default:
                GD.PushError($"Packet type with index {(int)packetType} unhandled!");
                break;
        }
    }


    private void ManageIds(IDAssignment idAssignment)
    {
        // local client ID
        if (_id == -1)
        {
            _id = idAssignment.Id;
            EmitSignal(SignalName.HandleLocalIdAssignment, _id);

            _remoteIds = idAssignment.RemoteIds;

            foreach (int remoteId in _remoteIds)
            {
                if (remoteId == _id)
                    continue;

                EmitSignal(SignalName.HandleRemoteIdAssignment, remoteId);
            }
        }
        // new remote peers
        else
        {
            _remoteIds.Add(idAssignment.Id);
            EmitSignal(SignalName.HandleRemoteIdAssignment, idAssignment.Id);
        }
    }
}
