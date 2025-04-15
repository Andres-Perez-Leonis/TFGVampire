using System.Threading.Tasks;
using Godot;

public partial class VampireDraggingState : VampireStateBase
{
  [Export] private RayCastNPCDetector _detector;
  [Export] private HidingPointDetector _corpseHidingPointDetector;


  private NPC _corpseNPC;
  private bool _isStatingState = true;

  /***
     * The current speed of the vampire while moving.
     */
  private float _currentSpeed;

  /***
   * The direction the vampire is moving in (left or right).
   */
  private float _direction = 0f;

  /***
   * The velocity vector representing the vampire's movement.
   */
  private Vector2 _velocity;

  public async Task WaitAsync() {
    SceneTreeTimer timer = GetTree().CreateTimer(1.0f);
    timer.Timeout += () => { _isStatingState = false; };
    await ToSignal(timer, SceneTreeTimer.SignalName.Timeout);
    //GD.Print("Termino el inicio de este estado");
  }

  private void UnlockChangeState() {
    _isStatingState = true;
  }
  
  public override async void OnInput(InputEvent @event)
  {
    await WaitAsync();
    //GD.Print("Pulso de la letra E: ", Input.IsPhysicalKeyPressed(Key.E));
    //GD.Print("Comienzo de estado: ", _isStatingState);
    if(!Input.IsPhysicalKeyPressed(Key.E) || _isStatingState) return;

    _corpseHidingPointDetector.CorpseHidingPoint?.HideCorpse(_corpseNPC);
    StateMachine.ChangeState(VampireStateNames.Idle);
  }

  public override void Start()
  {

    if (!_detector.IsColliding())
    {
      StateMachine.ChangeState(VampireStateNames.Idle);
      //GD.Print("NO COLISIONA");
    }

    _corpseNPC = _detector.NPCDetected;
    _isStatingState = true;
  }

  public override void _Ready()
  {
    base._Ready();
    _currentSpeed = 0.6f * _vampire.Speed;
  }


  public override void OnPhysicsProcess(double delta)
  {
    base.OnPhysicsProcess(delta);

    // Gets the direction based on player input (left or right arrow keys)
    _direction = Input.GetAxis("ui_left", "ui_right");


    // Update the vampire's velocity in the X direction based on input
    _velocity.X = _direction * _currentSpeed;

    // Set the vampire's velocity and apply the movement
    _vampire.Velocity = _velocity;
    _corpseNPC.Velocity = _velocity;
    _vampire.MoveAndSlide();
    _corpseNPC.MoveAndSlide();

  }



}
