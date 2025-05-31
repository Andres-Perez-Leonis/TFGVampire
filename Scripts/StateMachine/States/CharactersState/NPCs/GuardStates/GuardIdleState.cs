using Godot;
using System;

public partial class GuardIdleState : GuardStateBase
{
    public override void _Ready()
    {
        base._Ready();
        _guard.NotifyCorpseFound += GoToCorpse;
    }
    public override void Start()
    {
        base.Start();
        _guard.VampireDetected += VampireDetected;
    }

    public override void End()
    {
        base.End();
        _guard.VampireDetected -= VampireDetected;
    }



    private void VampireDetected()
    {
        StateMachine.ChangeState(GuardStateNames.Attack);
    }

    private void GoToCorpse()
    {
        //_guard.Destination = _corpseToCheck.PathFollow.LastPassMarker;
        StateMachine.ChangeState(GuardStateNames.Moving);
    }


    public override void OnPhysicsProcess(double delta) { }
}
