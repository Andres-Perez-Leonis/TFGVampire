using Godot;
using System;

public partial class GuardMovingState : GuardMovingStateBase
{
    [Export] RayCast2D _corpseDetector;
    
    private bool _inTheSamePath = false;


    public override void OnPhysicsProcess(double delta)
    {
        base.OnPhysicsProcess(delta);

        bool nextToTheCorpse = _corpseDetector.IsColliding() || true;
        GD.Print("InTheSamePath: " + _inTheSamePath);
        GD.Print("nextToTheCorpse: " + nextToTheCorpse);
        if (!_inTheSamePath) CheckPath();
        if (nextToTheCorpse && _inTheSamePath)
            StateMachine.ChangeState(GuardStateNames.Drag);
    }

    public override void Start()
    {
        base.Start();
        _guard.VampireDetected += VampireDetected;
        _corpseDetector.ProcessMode = ProcessModeEnum.Inherit;
        CheckPath();
    }

    public override void End()
    {
        base.End();
        _corpseDetector.ProcessMode = ProcessModeEnum.Disabled;
        _guard.VampireDetected -= VampireDetected;
    }


    private void CheckPath()
    {
    }
    
    private void VampireDetected()
    {
        StateMachine.ChangeState(GuardStateNames.Attack);
    }

    private void InTheSamePath()
    {

    }

    protected override void InMyDestination()
    {
    }

}
