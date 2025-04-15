using Godot;

public partial class VampireMovingState : VampireStateBase
{
    /***
     * The current speed of the vampire while moving.
     */
    protected float _currentSpeed;

    /***
     * The direction the vampire is moving in (left or right).
     */
    private float _direction = 0f;

    /***
     * The velocity vector representing the vampire's movement.
     */
    protected Vector2 _velocity = Vector2.Zero;

    /***
     * Whether the vampire is running or walking.
     */
    private bool _isRunning = false;

    /***
     * Called when the node enters the scene tree for the first time.
     * Initializes the vampire's speed to the current speed.
     */
    public override void _Ready()
    {
        base._Ready();
        _currentSpeed = _vampire.Speed;
    }

    /***
     * Called during the physics process step.
     * Handles the vampire's movement, direction, speed, and state changes.
     * Updates the vampire's velocity based on player input.
     * Changes the state to idle if no movement is detected.
     * @param delta The time elapsed since the last physics frame.
     */
    public override void OnPhysicsProcess(double delta)
    {

        // Gets the direction based on player input (left or right arrow keys)
        _direction = Input.GetAxis("ui_left", "ui_right");

        // If no movement is detected, switch to the idle state
        if (_direction == 0) StateMachine.ChangeState(VampireStateNames.Idle);

        // Check if the run key (Shift) is pressed
        _isRunning = Input.IsKeyPressed(Key.Shift);

        // Set the current speed based on whether the vampire is running
        _currentSpeed = _isRunning ? _vampire.Speed * 1.5f : _vampire.Speed;

        // Update the vampire's velocity in the X direction based on input
        _velocity.X = _direction * _currentSpeed;

        // Set the vampire's velocity and apply the movement
        _vampire.Velocity = _velocity;
        _vampire.MoveAndSlide();
    }
}
