using Godot;

public partial class NpcMovingState : NPCMovingStateBase
{

    [Export] private HidingPointDetector _hidingPointDetector;

    /***
     * Called when the node enters the scene tree for the first time.
     * Initializes connections for the intersection detector and path follow events.
     */
    public override void _Ready()
    {
        base._Ready();
        
        GetNode<Area2D>("../../CorpseDetector/VisionConeArea").BodyEntered += OnBodyEnteredInCorpseVisionCone;
        GetNode<Area2D>("../../VampireDetector/VisionConeArea").BodyEntered += OnBodyEnteredInVampireDetector;
    }

    private void OnBodyEnteredInCorpseVisionCone(Node2D node) {
        //Emitir señal de haber encontrado un cuerpo
        if(node == _npc) return;
        if(node is not NPC) return;
        if(((NPC) node).IsHide) return;
        GD.Print("He visto un cadaver");
        StateMachine.ChangeState(NpcStateNames.GivingAlarm);
    }

     private void OnBodyEnteredInVampireDetector(Node2D node) {
        
        if(node is not Vampire) return;
        Vampire vampire = (Vampire) node;
        StateMachine stateMachine = vampire.GetNode<StateMachine>(".");
        if(stateMachine.CurrentState.Name != VampireStateNames.Attack) return;
        StateMachine.ChangeState(NpcStateNames.GivingAlarmRunning);
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

        
        if(_hidingPointDetector.HidingPoint != null) nodeFounded = _hidingPointDetector.HidingPoint.Interact();
        if(nodeFounded != 0) StateMachine.ChangeState( nodeFounded == 1 ? NpcStateNames.GivingAlarmRunning : NpcStateNames.Weird); 


        base.OnPhysicsProcess(delta);

            
    }

    protected override void InMyDestination()
    {
        // Transition to connected states based on destination
        _npc.NextTask();
        //NextPath();
        //StateMachine.ChangeState(NpcStateNames.Idle);
    }

}
