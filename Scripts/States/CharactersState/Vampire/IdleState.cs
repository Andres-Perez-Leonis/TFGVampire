using Godot;

public partial class IdleState : VampireStateBase
{

    public override void OnInput(InputEvent @event)
    {
		if(Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right")) StateMachine.ChangeState(VampireStateNames.Moving);
    }


}
