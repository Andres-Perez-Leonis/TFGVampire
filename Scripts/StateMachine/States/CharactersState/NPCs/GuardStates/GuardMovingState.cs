using Godot;
using System;

public partial class GuardMovingState : GuardMovingStateBase
{
    [Export] RayCast2D _corpseDetector;
    
    private bool _inTheSamePath = false;



    public override void OnPhysicsProcess(double delta)
    {
        base.OnPhysicsProcess(delta);

        bool nextToTheCorpse = _corpseDetector.IsColliding();

        if (!_inTheSamePath) CheckPath();
        if (nextToTheCorpse && _inTheSamePath)
            StateMachine.ChangeState(GuardStateNames.Drag);
    }

    public override void Start()
    {
        base.Start();
        CheckPath();
    }

    private void CheckPath() {
        _inTheSamePath = _guard.CorpseToCheck.PathFollow.GetParent<Path2D>() == _guard.PathFollow.GetParent<Path2D>();
    }
    
    



    private void InTheSamePath()
    {

    }

    protected override void InMyDestination()
    {
        Path2D nextPath = _guard.CorpseToCheck.PathFollow.GetParent<Path2D>();
        _guard.PathFollow.EmitOnChangePathSignal(nextPath.ToGlobal(nextPath.Curve.GetPointPosition(nextPath.Curve.PointCount - 1)));
        _guard.PathFollow.GetParent<Path2D>().RemoveChild(_guard.PathFollow);
        _guard.CorpseToCheck.PathFollow.GetParent<Path2D>().AddChild(_guard.PathFollow);
    }

}
