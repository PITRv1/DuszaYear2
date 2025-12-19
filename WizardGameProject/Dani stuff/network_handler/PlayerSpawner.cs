using Godot;
using System;
using System.Data.Common;

public partial class PlayerSpawner : Node
{
    [Export] PackedScene PlayerScene;
    [Export] Marker3D lookPosition;
    [Export] Godot.Collections.Array<Marker3D> Seats;

    public override void _Ready()
    {
        // Connect("OnPeerConnectedEventHandler", SpawnLobotomizedPlayer);
    }

    private void SpawnLobotomizedPlayer(int id)
    {
        MeshInstance3D meshInstance = new();
        meshInstance.Mesh = new CapsuleMesh();
        meshInstance.Position += new Vector3(0.0f, 1.0f, 0.0f);

        Seats[id].CallDeferred("AddChild", meshInstance);
    }

    private void SpawnPlayer(int id)
    {
        StaticBody3D player = (StaticBody3D)PlayerScene.Instantiate();

    }
}
