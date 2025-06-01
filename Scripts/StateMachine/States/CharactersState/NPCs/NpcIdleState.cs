using Godot;

public partial class NpcIdleState : VillagerStateBase
{
    public override void Start()
    {
        base.Start();
        //_npc.AnimationPlayer.Play(AnimationNameNPC.Idle);
        if(_npc.GetNode<VillagerDetector>("VillagerDetector").Villager != null) StateMachine.ChangeState(NpcStateNames.Talking);
    }


    public override void OnPhysicsProcess(double delta)
    {
        if(_npc.PathFollow.LastPassMarker != _npc.CurrentAction) StateMachine.ChangeState(NpcStateNames.Moving);
        GD.Print($"Soy {_npc.Name} estoy buscando {_npc.CurrentAction.Name} y vengo de {_npc.PathFollow.LastPassMarker.Name}");
    }
}
