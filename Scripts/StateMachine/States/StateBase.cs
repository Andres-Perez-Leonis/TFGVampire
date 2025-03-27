using Godot;

public partial class StateBase : Node
{
    /***
     * The node being controlled by this state (the owner of the state).
     */
    private Node _controlledNode;

    /***
     * The state machine that owns this state.
     */
    private StateMachine _stateMachine;

    /***
     * Called when the node enters the scene tree.
     * Initializes the controlled node to be the owner of the state.
     */
    public override void _Ready() {
        base._Ready();
        _controlledNode = this.Owner;
    }

    #region Common Methods of State Machine

    /***
     * Starts the state. This method can be overridden to initialize state-specific logic.
     */
    public virtual void Start() {}

    /***
     * Ends the state. This method can be overridden to handle cleanup or transition logic.
     */
    public virtual void End() {}

    /***
     * Called during the process step.
     * This method can be overridden to add logic specific to the process step.
     * @param delta The time elapsed since the last frame.
     */
    public virtual void OnProcess(double delta) {}

    /***
     * Called during the physics process step.
     * This method can be overridden to add logic specific to the physics process.
     * @param delta The time elapsed since the last physics frame.
     */
    public virtual void OnPhysicsProcess(double delta) {}

    /***
     * Handles input events for this state.
     * This method can be overridden to respond to input events.
     * @param @event The input event to handle.
     */
    public virtual void OnInput(InputEvent @event) {}

    /***
     * Handles unhandled input events for this state.
     * This method can be overridden to respond to unhandled input events.
     * @param @event The unhandled input event.
     */
    public virtual void OnUnhandledInput(InputEvent @event) {}

    /***
     * Handles unhandled key input events for this state.
     * This method can be overridden to respond to unhandled key input events.
     * @param @event The unhandled key input event.
     */
    public virtual void OnUnhandledKeyInput(InputEvent @event) {}

    #endregion

    #region Getters and Setters

    /***
     * Gets or sets the node controlled by this state.
     */
    public Node ControlledNode
    {
        get => _controlledNode;

        set => _controlledNode = value;

    }

    /***
     * Gets or sets the state machine associated with this state.
     */
    public StateMachine StateMachine
    {
        get => _stateMachine;

        set => _stateMachine = value;

    }

    #endregion
}
