using Godot;

public partial class NpcIdleState : VillagerStateBase
{
    public override void Start()
    {
        base.Start();
        //_npc.AnimationPlayer.Play(AnimationNameNPC.Idle);
    }


    public override void OnPhysicsProcess(double delta)
    {
        if(_npc.PathFollow.LastPassMarker != _npc.CurrentAction) StateMachine.ChangeState(NpcStateNames.Moving);
        GD.Print($"Soy {_npc.Name} estoy buscando {_npc.CurrentAction.Name} y vengo de {_npc.PathFollow.LastPassMarker.Name}");
    }
}
