using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class GivingAlarmRunningState : NPCMovingStateBase
{
    //[Signal] public delegate void CorpseFoundedEventHandler(Node2D node2D);

    [Export] private int _guardWarnCount = 2;
    public override void Start()
    {
        //_npc.AnimationPlayer.Play(AnimationNameNPC.Shout);
        List<Node> guards = GetTree().GetNodesInGroup(NameGroups.GuardGroup).ToList();

        List<Node2D> nearestGuards = guards.OfType<Node2D>()
            .OrderBy(guard => _npc.GlobalPosition.DistanceSquaredTo(guard.GlobalPosition))
            .Take(_guardWarnCount).ToList();
        GD.Print("Progress: " + _npc.PathFollow.ProgressRatio);

        foreach (Guard guard in nearestGuards)
        {
            guard.EmitVampireDetectedSignal();
        }
        rotate();
        _npc.Speed += 0.1f;
        //_progressInDown = !_progressInDown;
        _npc.ToHome();
    }

    public override void OnPhysicsProcess(double delta)
    {
        base.OnPhysicsProcess(delta);
        
        GD.Print("Progress: " + _npc.PathFollow.ProgressRatio);
    }

    public override void End()
    {
        base.End();
        _npc.Speed -= 0.1f;
    }

    protected override void InMyDestination()
    {
    }
}
