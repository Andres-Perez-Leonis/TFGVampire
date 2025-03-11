using Godot;

public partial class MovingState : VampireStateBase
{
	private float _currentSpeed;
	private float _direction = 0f;
	//private float _gravity = (float) ProjectSettings.GetSetting("physics/2d/default_gravity");

	private Vector2 _velocity;

	private bool _isRunning = false;
    //private bool _wasRunningWhenJumped = false;
	public override void _Ready()
	{
		base._Ready();
		_currentSpeed = _vampire.Speed;
	}

/*
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
*/

    public override void OnPhysicsProcess(double delta)
    {
		_velocity = _vampire.Velocity;

		_direction = Input.GetAxis("ui_left", "ui_right");

		if(_direction == 0) StateMachine.ChangeState(VampireStateNames.Idle);
		//GD.Print("Direction: " + _direction);

		_isRunning = Input.IsKeyPressed(Key.Shift);

		_currentSpeed = _isRunning ? _vampire.Speed * 1.5f : _vampire.Speed;
		//UpdateSpeed();

		_velocity.X = _direction * _currentSpeed;

		//if(_velocity.X == 0) StateMachine.ChangeState(VampireStateNames.Idle);

		_vampire.Velocity = _velocity;
		
		_vampire.MoveAndSlide();
    }
}
