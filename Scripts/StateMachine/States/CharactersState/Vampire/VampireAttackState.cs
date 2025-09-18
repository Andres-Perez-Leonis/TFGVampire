using System;
using Godot;
public partial class VampireAttackState : VampireStateBase
{
  [Export] private RayCastVillagerDetector _detector;

  [Export] private AudioStreamPlayer _attackAudio;
  [Export] private AudioStreamPlayer _drinkAudio;

  public override void _Ready()
  {
    base._Ready();

    //_vampire.AnimationPlayer.AnimationFinished += OnAnimationFinished;
  }

  Villager villager;
  public override void Start()
  {
    villager = _detector.VillagerDetected;
    if(villager == null || villager.IsDeath) { StateMachine.ChangeState(VampireStateNames.Idle); return;}
    _vampire.AnimationTree.AnimationFinished += OnAnimationFinished;
    _attackAudio.Play();
    _vampire.AnimationStateMachine.Travel(AnimationNameVampire.Attacking);
    _drinkAudio.Play();
    villager.Visible = false;
    villager.EmitIamOnAttackSignal();
    _vampire.GlobalPosition = new Vector2(villager.GlobalPosition.X, _vampire.GlobalPosition.Y);
    GetTree().CreateTimer(1.5).Timeout += ReturnToIdleState;
  }


  private void ReturnToIdleState()
  {
    StateMachine.ChangeState(VampireStateNames.Idle);
    villager.Visible = true;

  }

  public override void End()
  {
    //_vampire.AnimationTree.AnimationFinished -= OnAnimationFinished;
  }


  private void OnAnimationFinished(StringName animationName)
  {
    StateMachine.ChangeState(VampireStateNames.Idle);
  }
}