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
    [Signal]
    public delegate void HandleTurnInfoEventHandler(byte[] data);
    [Signal]
    public delegate void HandlePickUpCardAnswerEventHandler(byte[] data);
    [Signal]
    public delegate void NewPlayerEventHandler(byte[] data);
    [Signal]
    public delegate void StartGameEventHandler();
    [Signal]
    public delegate void CursonUpdateEventHandler(byte[] data);
    [Signal]
    public delegate void ShopSceneEventHandler();

    public int _id = -1;
    // must be readonly
    public List<int> _remoteIds = new();

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
            case PACKET_TYPES.START_GAME:
                // Is that possible you put these signal names into its own class as a public static var?
                // So if you change the class name, you change there its signal name as well.
                EmitSignal("StartGame");
                break;
            case PACKET_TYPES.NEW_PLAYER:
                EmitSignal("NewPlayer", data);
                break;
            case PACKET_TYPES.TURN_INFO:
                EmitSignal("HandleTurnInfo", data);
                break;
            case PACKET_TYPES.PICK_UP_CARD_ANSWER:
                GD.Print("so this prints"); // So this is a dead code that smells as sh_t
                EmitSignal("HandlePickUpCardAnswer", data);
                break;
            case PACKET_TYPES.CURSOR_UPDATE:
                EmitSignal("CursonUpdate", data);
                break;
            case PACKET_TYPES.SHOP_SCENE_CHANGE:
                EmitSignal("ShopScene");
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
        // you need no else block here. Just do a return at the and of the if part, and then remove the else {} and leave its code 
        // as it is
        else
        {
            _remoteIds.Add(idAssignment.Id);
            EmitSignal(SignalName.HandleRemoteIdAssignment, idAssignment.Id);
        }
    }
}
