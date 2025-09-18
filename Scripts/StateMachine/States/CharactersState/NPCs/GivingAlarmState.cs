using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class GivingAlarmState : VillagerStateBase
{
    //[Signal] public delegate void CorpseFoundedEventHandler(Node2D node2D);
    [Export] private VillagerDetector _detector;
    private NPC _corpseFounded;
    [Export] private Sprite2D _exclamacion;
    private Timer _timeToIdle;
    [Export] float offset = 0.2f;
    private bool _progressInDown = false;
    public override void Start()
    {
        base.Start();
        //_npc.AnimationPlayer.Play(AnimationNameNPC.Shout);
        //_villager.AnimationStateMachine.Travel(AnimationNameNPC.Moving);
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
        _exclamacion.Visible = true;
        _npc.Speed *= 2f;
        _timeToIdle.Start();
    }

    public override void _Ready()
    {
        base._Ready();
        _timeToIdle = _villager.GetNode<Timer>("Timer");
        _timeToIdle.Timeout += ReturnToIdleState;
        //CorpseFounded += OnCorpseFounded;
    }

    private void OnCorpseFounded(Node2D corpse)
    {
        _corpseFounded = (NPC)corpse;
    }

    public void CorpseFounded(NPC npcCorpse)
    {
        _corpseFounded = npcCorpse;
    }

    private void ReturnToIdleState()
    {
        _exclamacion.Visible = false;
        StateMachine.ChangeState(NpcStateNames.Moving);
    }

    public override void OnPhysicsProcess(double delta)
    {
        base.OnPhysicsProcess(delta);
        float progress = (float)delta * _npc.Speed;

    }

}
