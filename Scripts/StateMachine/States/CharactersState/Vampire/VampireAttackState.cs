using System;
using Godot;
public partial class VampireAttackState : VampireStateBase
{
  [Export] private RayCastNPCDetector _detector;

  [Export] private AudioStreamPlayer _attackAudio;
  [Export] private AudioStreamPlayer _drinkAudio;

  public override void _Ready()
  {
    base._Ready();

    //_vampire.AnimationPlayer.AnimationFinished += OnAnimationFinished;
  }


  public override void Start()
  {
    NPC npc = _detector.NPCDetected;
    if(npc == null || npc.IsDeath) { StateMachine.ChangeState(VampireStateNames.Idle); return;}
    _vampire.AnimationTree.AnimationFinished += OnAnimationFinished;
    _attackAudio.Play();
    _vampire.AnimationStateMachine.Travel(AnimationNameVampire.Attacking);
    _drinkAudio.Play();
    npc.EmitIamOnAttackSignal();
    _vampire.GlobalPosition = new Vector2(npc.GlobalPosition.X, _vampire.GlobalPosition.Y);
    GetTree().CreateTimer(1.5).Timeout += ReturnToIdleState;
  }


  private void ReturnToIdleState()
  {
    StateMachine.ChangeState(VampireStateNames.Idle);
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