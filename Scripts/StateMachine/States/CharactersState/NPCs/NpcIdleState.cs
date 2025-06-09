using Godot;

public partial class NpcIdleState : NpcStateBase
{
    public override void Start()
    {
        base.Start();
        //_npc.AnimationPlayer.Play(AnimationNameNPC.Idle);
        string nextState = NpcStateNames.Moving;
        if (_npc.GetNode<VillagerDetector>("VillagerDetector").Villager != null) nextState = NpcStateNames.Talking;
        //else if (_npc.PathFollow.LastPassMarker != _npc.CurrentAction) StateMachine.ChangeState(NpcStateNames.Moving);
        StateMachine.ChangeState(nextState);
    }


}
