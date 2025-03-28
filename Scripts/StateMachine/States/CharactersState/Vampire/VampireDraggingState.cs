using Godot;

public partial class VampireDraggingState : VampireMovingState
{
	[Export] private RayCastNPCDetector _detector;
	[Export] private Area2D _hiddingPointDetector;
	private NPC _corpseNPC;
    public override void OnInput(InputEvent @event)
    {
		if(Input.IsPhysicalKeyPressed(Key.E)) {
			string state = _hiddingPointDetector.HasOverlappingAreas() ? VampireStateNames.HidingCorpse : VampireStateNames.Idle;
			
			StateMachine.ChangeState(state);
		}
    }

    public override void Start()
    {
		if(!_detector.IsColliding()) StateMachine.ChangeState(VampireStateNames.Idle);

		_corpseNPC = _detector.NPCDetected;
    }

    public override void _Ready()
    {
        base._Ready();
		_currentSpeed *= 0.5f;
    }


    public override void OnPhysicsProcess(double delta)
    {
        base.OnPhysicsProcess(delta);
		_corpseNPC.Velocity = _velocity;
		_corpseNPC.MoveAndSlide();

    }


}
