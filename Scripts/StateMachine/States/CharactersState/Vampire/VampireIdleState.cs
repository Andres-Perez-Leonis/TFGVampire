using Godot;

public partial class VampireIdleState : VampireStateBase
{
    /***
     * Called when an input event is detected.
     * Switches the state to the VampireMovingState if left or right input is pressed.
     * @param @event The input event that triggered this method.
     */
    public override void OnInput(InputEvent @event)
    {
        // If the left or right input action is pressed, change to the moving state
        if (Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right"))
            StateMachine.ChangeState(VampireStateNames.Moving);
        if(Input.IsKeyPressed(Key.F))
            StateMachine.ChangeState(VampireStateNames.Attack);
    }

    public override void Start()
    {
        base.Start();
        //_vampire.AnimationPlayer.Play(AnimationNameVampire.Idle);
    }

}
