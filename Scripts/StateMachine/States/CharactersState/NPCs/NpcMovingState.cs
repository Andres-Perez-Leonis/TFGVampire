using System.Linq;
using System.Reflection;
using Godot;

public partial class NpcMovingState : NPCMovingStateBase
{

    [Export] private HidingPointDetector _hidingPointDetector;

    private Vampire _vampire;

    /***
     * Called when the node enters the scene tree for the first time.
     * Initializes connections for the intersection detector and path follow events.
     */


    //static int i = 0;
    public override void _Ready()
    {
        /*
        if (i == 0)
        {
            for (int i = 1; i <= 20; i++)
            {
                string name = GetLayerName(i);
                GD.Print($"Layer {i}: {name}");
            }
        }
        */

        base._Ready();
        GetNode<Area2D>("../../VampireDetector/VisionConeArea").CollisionMask = IntLayerMarks.Vampire;
        GetNode<Area2D>("../../CorpseDetector/VisionConeArea").CollisionMask = IntLayerMarks.Corpse;
    }


    public override void Start()
    {
        base.Start();
        
        GetNode<Area2D>("../../VampireDetector/VisionConeArea").BodyEntered += OnBodyEnteredInVampireDetector;
        GetNode<Area2D>("../../VampireDetector/VisionConeArea").BodyExited += OnBodyExitedInVampireDetector;
        GetNode<Area2D>("../../CorpseDetector/VisionConeArea").BodyEntered += OnBodyEnteredInCorpseVisionCone;
        _npc.GetNode<VillagerDetector>("VillagerDetector").BodyEntered += ReturnToIdleState;

    }

    public override void End()
    {
        base.End();
        
        GetNode<Area2D>("../../VampireDetector/VisionConeArea").BodyEntered -= OnBodyEnteredInVampireDetector;
        GetNode<Area2D>("../../VampireDetector/VisionConeArea").BodyExited -= OnBodyExitedInVampireDetector;
        GetNode<Area2D>("../../CorpseDetector/VisionConeArea").BodyEntered -= OnBodyEnteredInCorpseVisionCone;
        _npc.GetNode<VillagerDetector>("VillagerDetector").BodyEntered -= ReturnToIdleState;
    }

    private void ReturnToIdleState(Node2D node)
    {
        if (node == _npc) return;
        StateMachine.ChangeState(NpcStateNames.Idle);
    }


    private void OnBodyEnteredInVampireDetector(Node2D node)
    {

        GD.Print("El vampiro entra");
        if (node is not Vampire vampire) return;
        _vampire = vampire;
    }

    private void OnBodyExitedInVampireDetector(Node2D node)
    {

        GD.Print("El vampiro sale");
        if (node is not Vampire) return;
        _vampire = null;
    }

    private void OnBodyEnteredInCorpseVisionCone(Node2D node)
    {
        //Emitir señal de haber encontrado un cuerpo
        if (node == _npc) return;
        if (node is not NPC) return;
        if (((NPC)node).IsHide) return;
        
        if (_vampire != null)
        {
            StateMachine stateMachine = _vampire.GetNode<StateMachine>("./StateMachine");
            if (stateMachine.CurrentState.Name == VampireStateNames.Attack)
            {
                StateMachine.ChangeState(NpcStateNames.GivingAlarmRunning);
                return;
            }

        }

        GetNode<GivingAlarmState>("../GivingAlarmState").CorpseFounded((NPC)node);
        StateMachine.ChangeState(NpcStateNames.GivingAlarm);
    }

    /***
     * Called during the physics process step.
     * Updates the NPC's position along the path and triggers path transitions when necessary.
     * @param delta The time elapsed since the last physics frame.
     */
    public override void OnPhysicsProcess(double delta)
    {
        int nodeFounded = 0;

        //if(_npc.CurrentAction == _pathFollow.LastPassMarker) StateMachine.ChangeState(NpcStateNames.Idle);
        //GD.Print("Vampiro es nulo: " + _vampire?.Name);

        if (_hidingPointDetector.HidingPoint != null) nodeFounded = _hidingPointDetector.HidingPoint.Interact();
        if (nodeFounded != 0) StateMachine.ChangeState(nodeFounded == 1 ? NpcStateNames.GivingAlarmRunning : NpcStateNames.Weird);


        base.OnPhysicsProcess(delta);


    }

    protected override void InMyDestination()
    {
        // Transition to connected states based on destination
        _npc.NextTask();
        _agent.SetTargetPosition(_npc.CurrentAction.GlobalPosition);
        //NextPath();
        //StateMachine.ChangeState(NpcStateNames.Idle);
    }

/*
    private string GetLayerName(int layerNumber)
    {
        return ProjectSettings.GetSetting($"layer_names/2d_physics/layer_{layerNumber}").AsString();
    }
*/
}
