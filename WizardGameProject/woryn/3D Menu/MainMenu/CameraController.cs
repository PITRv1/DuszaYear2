using Godot;
using GodotPlugins.Game;
using System;

public partial class CameraController : Node
{
    [Export] private Camera3D animatedCamera {set;get;}
    [Export] private CombinedUI mainMenuUi {set;get;}

    [Export] private Godot.Collections.Array<Marker3D> focusPoints {set;get;}

    private Tween _tween;

    public override void _Ready()
    {
        mainMenuUi.MenuChanged += _OnMenuChanged;
    }

    public void FocusOnPoint(int focusPointIndex)
    {
        Marker3D focusPoint = focusPoints[focusPointIndex];        
        ResetTween();

        _tween.SetParallel(true);
        _tween.SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Quad);

        _tween.TweenProperty(animatedCamera,"global_position", focusPoint.GlobalPosition, 1.0f);
        _tween.TweenProperty(animatedCamera,"global_rotation", focusPoint.GlobalRotation, 1.0f);
    }

    private void _OnMenuChanged(Control newMenu)
    {
        switch (newMenu)
        {
            case var value when value == mainMenuUi.mainMenu:
                FocusOnPoint(0);
            break;

            case var value when value == mainMenuUi.settingsMenu:
                FocusOnPoint(1);
            break;


        }
    }

    private void ResetTween()
    {
        if (_tween != null)
            _tween.Kill();
        _tween = CreateTween();
    }

}
