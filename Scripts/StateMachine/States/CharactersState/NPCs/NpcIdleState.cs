using Godot;

public partial class NpcIdleState : NpcStateBase
{
    
    public override void OnPhysicsProcess(double delta)
    {
        //_npc.AnimationPlayer.Play(AnimationNameNPC.Idle);
        if(_npc.PathFollow.LastPassMarker != _npc.CurrentAction) StateMachine.ChangeState(NpcStateNames.Moving);
        GD.Print($"Soy {_npc.Name} estoy buscando {_npc.CurrentAction.Name} y vengo de {_npc.PathFollow.LastPassMarker.Name}");
    }
}
