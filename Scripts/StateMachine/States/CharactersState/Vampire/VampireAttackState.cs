using System;
using Godot;
public partial class VampireAttackState : VampireStateBase
{
  [Export] private RayCastNPCDetector _detector;

  public override void _Ready()
  {
    base._Ready();
    //_vampire.AnimationPlayer.AnimationFinished += OnAnimationFinished;
  }


  public override void Start()
  {
    _vampire.AnimationTree.AnimationFinished += OnAnimationFinished;
    _vampire.AnimationStateMachine.Travel(AnimationNameVampire.Attacking);
    NPC npc = _detector.NPCDetected;
    if(npc == null) { StateMachine.ChangeState(VampireStateNames.Idle); return;}
    if(npc.IsDeath) return;
    npc.EmitIamOnAttackSignal();
    _vampire.GlobalPosition = npc.GlobalPosition;
    GetTree().CreateTimer(1).Timeout += ReturnToIdleState;
  }

  private void ReturnToIdleState() {
    StateMachine.ChangeState(VampireStateNames.Idle);
  }

  public override void End()
  {
    _vampire.AnimationTree.AnimationFinished += OnAnimationFinished;
  }


  private void OnAnimationFinished(StringName animationName)
  {
    StateMachine.ChangeState(VampireStateNames.Idle);
  }
}