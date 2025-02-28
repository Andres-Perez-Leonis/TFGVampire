using Godot;

public partial class VampireMovement : Node
{
	[Export]
	private Vampire _vampire;

	private float _currentSpeed;
	private float _direction = 0f; 
	[Export]
	private double _gravity = 1000;

	private Vector2 _velocity;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		_currentSpeed = _vampire.Speed;
	}


	public void Running(bool isRunning) {
		_currentSpeed = isRunning ? _vampire.Speed * 1.5f : _vampire.Speed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
		
		_velocity = _vampire.Velocity;
		if(!_vampire.IsOnFloor()) {
			_velocity.Y += (float) (_gravity * delta);
		} 
		
		if(Input.IsActionJustPressed("ui_select") && _vampire.IsOnFloor()) {
			_velocity.Y = -_vampire.JumpForce;
		}

		_direction = Input.GetAxis("ui_left", "ui_right");

		_velocity.X = _direction * _currentSpeed;
		_vampire.Velocity = _velocity;
		_vampire.MoveAndSlide();
    }
}
