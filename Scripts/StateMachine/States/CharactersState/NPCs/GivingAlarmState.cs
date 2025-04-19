using Godot;

public partial class GivingAlarmState : NPCMovingStateBase
{
    [Export] private MarkerPathSwitch _guardPoint;


    public override void Start()
    {
        base.Start();
        _npc.AnimationPlayer.Play(AnimationNameNPC.Fleeing);
        _npc.Speed *= 2;
        _npc.Destination = _guardPoint;
    }


    public override void OnPhysicsProcess(double delta)
    {
        base.OnPhysicsProcess(delta);
    }

    protected override void InMyDestination()
    {
        // GUARDS ATTACK
    }

}
