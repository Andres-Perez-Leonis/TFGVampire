using Godot;
using System;

public partial class GuardIdleState : GuardStateBase
{
    public override void _Ready()
    {
        base._Ready();
        _guard.NotifyCorpseFound += GoToCorpse;
    }

    private void GoToCorpse(NPC corpse) {
        
        GD.Print("Me han llamado soy " + Name);
        _guard.CorpseToCheck = corpse;
        //_guard.Destination = _corpseToCheck.PathFollow.LastPassMarker;
        StateMachine.ChangeState(NpcStateNames.Moving);
    }

    public override void OnPhysicsProcess(double delta) {}
}
