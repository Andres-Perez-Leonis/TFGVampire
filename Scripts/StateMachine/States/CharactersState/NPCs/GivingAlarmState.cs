using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class GivingAlarmState : VillagerStateBase
{
    //[Signal] public delegate void CorpseFoundedEventHandler(Node2D node2D);
    [Export] private VillagerDetector _detector;
    private NPC _corpseFounded;
    public override void Start()
    {
        base.Start();
        //_npc.AnimationPlayer.Play(AnimationNameNPC.Shout);
        List<Node> guards = GetTree().GetNodesInGroup(NameGroups.GuardGroup).ToList();
        List<Node2D> nearestGuards = guards.OfType<Node2D>().OrderBy(guard => _npc.GlobalPosition.DistanceSquaredTo(guard.GlobalPosition)).Take(2).ToList();
        foreach (Guard guard in nearestGuards)
        {
            guard.ReportCorpseFound(_corpseFounded);
        }

        if (_detector.LastVillagerExposed == null)
        {
            GD.Print("Hubo un error y se ha perdido la referencia del ");
        }

        _villager.SuspicionSystem.IncreaseSuspision(_detector.LastVillagerExposed);
    }

    public override void _Ready()
    {
        base._Ready();
        //CorpseFounded += OnCorpseFounded;
    }

    private void OnCorpseFounded(Node2D corpse) {
        _corpseFounded = (NPC) corpse;
    }

    public void CorpseFounded(NPC npcCorpse)
    {
        _corpseFounded = npcCorpse;
    }

    public override void OnPhysicsProcess(double delta)
    {
        base.OnPhysicsProcess(delta);
    }

}
