using Godot;
using System;

public abstract partial class NPCMovingStateBase : VillagerStateBase
{

	
	/***
	 * The Area2D used to detect intersections for path switching.
	 */
	[Export] protected Area2D _interconectionDetector;
	protected bool _progressInDown = false;

	private MarkerPathSwitch _objective;

	protected static NavigationAgent2D _agent;



	/***
	 * Called when the node enters the scene tree for the first time.
	 * Initializes connections for the intersection detector and path follow events.
	 */
	public override void _Ready()
	{
		base._Ready();
		CallDeferred("CallDeferredReady");
	}

	public override void Start()
	{
		base.Start();
		_npc.AnimationStateMachine.Travel(AnimationNameNPC.Moving);
		//CheckOrientation(_npc.CurrentAction.GlobalPosition);
		
		_interconectionDetector.AreaEntered += MarkerSwitchDetected;
	}

	public override void End()
	{
		base.End();
		
		_interconectionDetector.AreaEntered -= MarkerSwitchDetected;
    }

	private void ReturnToIdleState(Node2D node)
	{
		StateMachine.ChangeState(NpcStateNames.Idle);
	}



	private void CallDeferredReady()
	{
		_agent = _npc.GetNode<NavigationAgent2D>("NavigationAgent2D");
		AssignTarget();
	}


	public override void OnPhysicsProcess(double delta)
	{
		base.OnPhysicsProcess(delta);
		if (_agent.IsNavigationFinished())
		{
			_npc.Velocity = Vector2.Zero;
			InMyDestination();
			return;
		}

		Vector2 nextPathPosition = _agent.GetNextPathPosition();
		CheckOrientation(nextPathPosition);

		//nextPathPosition.Y = _npc.GlobalPosition.Y; // Maintain the same Y level for 2D movement
		Vector2 direction = (nextPathPosition - _npc.GlobalPosition).Normalized();
		GD.Print($"NPC Position: {_npc.GlobalPosition}, Next Path Position: {nextPathPosition}, Direction: {direction}");
		_npc.Velocity = direction * _npc.Speed;

		_npc.MoveAndSlide();

		/*
		// Calculate the progress in the path
		float progress = (float)delta * _npc.Speed;
		_npc.PathFollow.ProgressRatio += (_progressInDown) ? -progress : progress;
		//GD.Print("Proogress Ratio: " + _npc.PathFollow.ProgressRatio);

		// Check if the NPC has reached the end or start of the path to trigger the next path
		if (_npc.PathFollow.ProgressRatio == 1 || _npc.PathFollow.ProgressRatio == 0) 
			NextPath();
		*/
	}

	private void AssignTarget()
	{
		Vector2 position = _npc.CurrentAction.GlobalPosition;
		position.Y = _npc.GlobalPosition.Y;
		_agent.SetTargetPosition(position);
	}

	/***
	 * Changes the NPC's path when the current path is completed.
	 * Ensures the path interconnector exists before proceeding.
	 */
	protected void NextPath()
	{
		if (_objective == null)
		{
			GD.Print("Error: Path Interconnector Undetected");
			return;
		}
		//_objective.ChangeTheNPCRoute(_npc.PathFollow, _npc.CurrentAction);
	}


	
	/***
	 * Placeholder method for actions when the NPC reaches its destination.
	 * Transitions to connected states such as Working, Keeping Object, Idle, or Talking.
	 */
	protected abstract void InMyDestination();


	
	/***
	 * Checks and updates the NPC's orientation based on the current and next path's direction.
	 * This adjusts the NPC's scale to reflect the correct orientation for movement.
	 * @param intialPointOfCurrentPath The starting point of the current path.
	 * @param finalPointOfNextPath The endpoint of the next path.
	 */
	private void CheckOrientation(Vector2 finalPointOfNextPath)
	{
		if (GetParent<StateMachine>().CurrentState != this) return;
		float distanceFinal = _npc.GlobalPosition.DistanceTo(finalPointOfNextPath);
		//GD.Print($"Distacia desde {_npc.GlobalPosition} al punto final {finalPointOfNextPath}: {distanceFinal}");


		float angle, direction;

		angle = _npc.GlobalPosition.AngleToPoint(_npc.CurrentAction.GlobalPosition);
		direction = Mathf.Sign(Mathf.Cos(angle));

		float scaleFactor = Mathf.Abs(_npc.Scale.X);
		if (_npc.Scale.X != direction * scaleFactor)
		{
			rotate();
		}
	}

	/***
	 * Rotates the NPC to face the opposite direction by flipping its scale on the X-axis.
	 */
	public void rotate()
	{
		_npc.Scale = new Vector2(_npc.Scale.X * -1, _npc.Scale.Y);
		_npc.GetNode<Node2D>("CorpseDetector").RotationDegrees *= -1;
		_npc.GetNode<Node2D>("VampireDetector").RotationDegrees *= -1;
	}

	

	/***
	 * Called when the interconnection detector (Area2D) detects a new marker path.
	 * If the marker path belongs to the NPC's work location, the state is changed to Working.
	 * @param area2D The Area2D that triggered the event.
	 */
	private void MarkerSwitchDetected(Area2D area2D)
	{
		MarkerPathSwitch markerPath = area2D.GetParent<MarkerPathSwitch>();
		if (markerPath == _npc.WorkPlace && _npc.WorkPlace == _npc.CurrentAction)
			StateMachine.ChangeState(NpcStateNames.Working);

		_objective = markerPath;
	}
}
