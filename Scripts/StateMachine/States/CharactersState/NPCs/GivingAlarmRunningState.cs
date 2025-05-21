using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class GivingAlarmRunningState : NPCMovingStateBase
{
    //[Signal] public delegate void CorpseFoundedEventHandler(Node2D node2D);

    private NPC _corpseFounded;
    public override void Start()
    {
        base.Start();
        if (_corpseFounded == null) GetTree().Quit();
        //_npc.AnimationPlayer.Play(AnimationNameNPC.Shout);
        List<Node> guards = GetTree().GetNodesInGroup(NameGroups.GuardGroup).ToList();
        List<Node2D> nearestGuards = guards.OfType<Node2D>().OrderBy(guard => _npc.GlobalPosition.DistanceSquaredTo(guard.GlobalPosition)).Take(2).ToList();
        foreach (Guard guard in nearestGuards)
        {
            guard.EmitNotifyCorpseFoundSignal(_corpseFounded);
        }

        _npc.Speed += 0.1f;
        _progressInDown = !_progressInDown;
        _npc.ToHome();
    }

    public override void End()
    {
        base.End();
        _npc.Speed -= 0.1f;
    }


    public override void _Ready()
    {
        base._Ready();
        GetNode<Area2D>("../../CorpseDetector/VisionConeArea").BodyEntered += OnCorpseFounded;
    }

    private void OnCorpseFounded(Node2D corpse) {
        _corpseFounded = (NPC) corpse;
    }

    public override void OnPhysicsProcess(double delta)
    {
        base.OnPhysicsProcess(delta);
    }

    protected override void InMyDestination()
    {
    }
}
