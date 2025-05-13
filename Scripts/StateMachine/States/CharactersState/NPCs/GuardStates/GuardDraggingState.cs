using Godot;
using System;

public partial class GuardDraggingState : GuardMovingStateBase
{
    private NPC _draggingCorpse;

    private MarkerPathSwitch _corpsesPoint;
    [Export] private float _nextToGuardOffset = 0.015f;

    public override void Start()
    {
        base.Start();
        _draggingCorpse = _guard.CorpseToCheck;
        _guard.Destination = _corpsesPoint;
        GD.Print("Yo " + Owner.Name + " tengo un progreso " + _guard.PathFollow.ProgressRatio);
        GD.Print("Yo " + "cadaver" + " tengo un progreso " + _draggingCorpse.PathFollow.ProgressRatio);
       //GetTree().Quit();
    }

    public override void _Ready()
    {
        base._Ready();
        _corpsesPoint = (MarkerPathSwitch)GetTree().GetFirstNodeInGroup(NameGroups.CorpsePoint);
    }


    protected override void InMyDestination()
    {

    }

    public override void OnPhysicsProcess(double delta)
    {
        base.OnPhysicsProcess(delta);
        _draggingCorpse.PathFollow.ProgressRatio = _guard.PathFollow.ProgressRatio - _nextToGuardOffset;

        if(_draggingCorpse.PathFollow.ProgressRatio == 1 || _draggingCorpse.PathFollow.ProgressRatio == 0) {
            _draggingCorpse.PathFollow.GetParent<Path2D>().RemoveChild(_draggingCorpse.PathFollow);
            _guard.PathFollow.GetParent<Path2D>().AddChild(_draggingCorpse.PathFollow);
        }

        
        GD.Print("Yo " + Owner.Name + " tengo un progreso " + _guard.PathFollow.ProgressRatio);
        GD.Print("Yo " + "cadaver" + " tengo un progreso " + _draggingCorpse.PathFollow.ProgressRatio);
            
    }

}
