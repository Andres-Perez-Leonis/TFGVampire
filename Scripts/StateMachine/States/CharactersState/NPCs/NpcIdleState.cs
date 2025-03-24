using Godot;

public partial class NpcIdleState : NpcStateBase
{
    
    public override void OnPhysicsProcess(double delta)
    {
        //_npc.AnimationPlayer.Play(AnimationNameNPC.Idle);
        if(_pathFollow.LastPassMarker != _npc.CurrentAction) StateMachine.ChangeState(NpcStateNames.Moving);
    }

    

}
