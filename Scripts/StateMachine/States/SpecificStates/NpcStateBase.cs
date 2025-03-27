using Godot;
using System;

public partial class NpcStateBase : StateBase
{
    /***
     * Reference to the NPC node that this state controls.
     */
    protected NPC _npc;

    /***
     * Reference to the NpcPathFollow node that controls the NPC's path.
     */
    protected NpcPathFollow _pathFollow;

    /***
     * Called when the node enters the scene tree for the first time.
     * Initializes the _npc reference and _pathFollow reference by retrieving the parent node of ControlledNode.
     */
    public override void _Ready()
    {
        base._Ready();
        _pathFollow = ControlledNode.GetParent<NpcPathFollow>();
        _npc = (NPC) ControlledNode;
        _npc.IamOnAttack += OnAttack;
    }

    /***
     * Called during the physics process step.
     * @param delta The time elapsed since the last physics frame.
     */
    public override void OnPhysicsProcess(double delta)
    {
        base.OnPhysicsProcess(delta);
        GD.Print("I am " + Name + " my state is : " + StateMachine.CurrentState.Name);
    }


    private void OnAttack() {
        StateMachine.ChangeState(NpcStateNames.Death);
    }
}
