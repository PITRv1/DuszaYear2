using Godot;
using System;

public partial class Gui : Node3D
{
    [Export] MeshInstance3D meshInstance;
    [Export] SubViewport subViewport;
    [Export] Area3D area3D;

    bool mouseEntered, mouseHeld, mouseInside = false;

    Vector3 lastMousePosition3D;
    Vector2 lastMousePosition2D;


    public override void _Ready()
    {
        area3D.MouseEntered += () => mouseEntered = true;
        subViewport.SetProcessInput(true);

    }

    public override void _PhysicsProcess(double delta)
    {
        
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        bool isMouseEvent = false;

        if (@event is InputEventMouse || @event is InputEventMouseButton) isMouseEvent = true;

        if (mouseEntered && (isMouseEvent || mouseHeld)) HandleMouse(@event);
        else if (!isMouseEvent) subViewport.PushInput(@event);
    }

    private void HandleMouse(InputEvent @event)
    {
        if (@event is InputEventMouseButton) mouseHeld = @event.IsPressed();

        Vector3 mousePosition3D = FindMouse(((InputEventMouseButton)@event).GlobalPosition);

        bool mouseInside = !(mousePosition3D == Vector3.Zero);

        if (mouseInside)
        {
            mousePosition3D *= area3D.GlobalTransform.AffineInverse();
            lastMousePosition3D = mousePosition3D; 
        } else
        {
            mousePosition3D = lastMousePosition3D;
            // if (mousePosition3D == Vector3.Zero)
            // {
            //     mousePosition3D = Vector3.Zero;
            // }
        }
        Vector2 mousePosition2D = new(mousePosition3D.X, -mousePosition3D.Y);
        Godot.Vector2 meshSize = (Godot.Vector2)meshInstance.Mesh.Get("size");
        mousePosition2D.X += meshSize.X / 2;
        mousePosition2D.Y += meshSize.Y / 2;

        mousePosition2D.X /= meshSize.X * subViewport.Size.X;
        mousePosition2D.Y /= meshSize.Y * subViewport.Size.Y;





    }

    private Vector3 FindMouse(Vector2 eventPosition)
    {
        
    }
}
