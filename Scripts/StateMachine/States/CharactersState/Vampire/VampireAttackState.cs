using System;
using Godot;
public partial class VampireAttackState : VampireStateBase
{
  [Export] private RayCastNPCDetector _detector;

  public override void _Ready()
  {
    base._Ready();
    _vampire.AnimationPlayer.AnimationFinished += OnAnimationFinished;
  }


  public override void Start()
  {
    //_vampire.AnimationPlayer.Play(AnimationNameVampire.Attacking);
    NPC npc = _detector.NPCDetected;
    if(npc == null) { StateMachine.ChangeState(VampireStateNames.Idle); return;}
    npc.EmitIamOnAttackSignal();
    _vampire.GlobalPosition = npc.GlobalPosition;
    StateMachine.ChangeState(VampireStateNames.Idle);
  }

  public override void End()
  {

  }


  private void OnAnimationFinished(StringName animationName)
  {
    StateMachine.ChangeState(VampireStateNames.Idle);
  }
}