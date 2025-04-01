using Godot;

public partial class NpcMovingState : NpcStateBase
{
    /***
     * The Area2D used to detect intersections for path switching.
     */
    [Export]
    protected Area2D _interconectionDetector;

    /***
     * The MarkerPathSwitch used to change the NPC's path when needed.
     */
    private MarkerPathSwitch _markerPathSwitch;

    /***
     * Boolean flag to determine the direction of the NPC's movement along the path.
     */
    private bool _progressInDown = false;

    /***
     * Called when the node enters the scene tree for the first time.
     * Initializes connections for the intersection detector and path follow events.
     */
    public override void _Ready()
    {
        base._Ready();
        _interconectionDetector.AreaEntered += MarkerSwitchDetected;
        _pathFollow.OnChangedPath += CheckOrientation;
        _pathFollow.InMyDestination += InMyDestination;
    }

    /***
     * Called during the physics process step.
     * Updates the NPC's position along the path and triggers path transitions when necessary.
     * @param delta The time elapsed since the last physics frame.
     */
    public override void OnPhysicsProcess(double delta)
    {
        base.OnPhysicsProcess(delta);


        //if(_npc.CurrentAction == _pathFollow.LastPassMarker) StateMachine.ChangeState(NpcStateNames.Idle);

        // Calculate the progress in the path
        float progress = (float)delta * _npc.Speed;
        _pathFollow.ProgressRatio += (_progressInDown) ? -progress : progress;
        //GD.Print("Proogress Ratio: " + _pathFollow.ProgressRatio);

        // Check if the NPC has reached the end or start of the path to trigger the next path
        if (_pathFollow.ProgressRatio == 1 || _pathFollow.ProgressRatio == 0) 
            NextPath();

            
    }

    /***
     * Changes the NPC's path when the current path is completed.
     * Ensures the path interconnector exists before proceeding.
     */
    private void NextPath()
    {
        if (_markerPathSwitch == null)
        {
            GD.Print("Error: Path Interconnector Undetected");
            return;
        }
        _markerPathSwitch.ChangeTheNPCRoute(_pathFollow, _npc.CurrentAction);
    }

    /***
     * Checks and updates the NPC's orientation based on the current and next path's direction.
     * This adjusts the NPC's scale to reflect the correct orientation for movement.
     * @param intialPointOfCurrentPath The starting point of the current path.
     * @param finalPointOfNextPath The endpoint of the next path.
     */
    private void CheckOrientation(Vector2 finalPointOfNextPath)
    {
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

        _progressInDown = distanceFinal < 50;
        //GD.Print("InDown: " + _progressInDown);
        _pathFollow.ProgressRatio = _progressInDown ? 1 : 0;
        //GD.Print("Proogress Ratio on Change: " + _pathFollow.ProgressRatio);
    }

    /***
     * Rotates the NPC to face the opposite direction by flipping its scale on the X-axis.
     */
    public void rotate()
    {
        _npc.Scale = new Vector2(_npc.Scale.X * -1, _npc.Scale.Y);
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
        
        _markerPathSwitch = markerPath;
    }

    /***
     * Placeholder method for actions when the NPC reaches its destination.
     * Transitions to connected states such as Working, Keeping Object, Idle, or Talking.
     */
    private void InMyDestination()
    {
        // Transition to connected states based on destination
        _npc.NextTask();
        //StateMachine.ChangeState(NpcStateNames.Idle);
    }
}
