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

	private bool _isRunning = false;
    private bool _wasRunningWhenJumped = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		_currentSpeed = _vampire.Speed;
	}


	public void UpdateSpeed() {
		if (_vampire.IsOnFloor())
        {
            _currentSpeed = _isRunning ? _vampire.Speed * 1.5f : _vampire.Speed;
            _wasRunningWhenJumped = _isRunning; // Guarda el estado de correr
        }
        else
        {
            // Si está en el aire, mantén la velocidad según si estaba corriendo al saltar
            _currentSpeed = _wasRunningWhenJumped ? _vampire.Speed * 1.5f : _vampire.Speed;
        }
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

		_isRunning = Input.IsKeyPressed(Key.Shift);

		UpdateSpeed();

		_velocity.X = _direction * _currentSpeed;
		_vampire.Velocity = _velocity;
		_vampire.MoveAndSlide();
    }
}
