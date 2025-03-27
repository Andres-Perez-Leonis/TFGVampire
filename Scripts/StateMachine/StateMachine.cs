using Godot;

public partial class StateMachine : Node
{
    /***
     * The initial state to be set when the scene is ready.
     */
    [Export]
    private StateBase _defaultState;

    /***
     * The currently active state.
     */
    private StateBase _currentState;

    /***
     * The node being controlled by this state machine (if applicable).
     */
    private Node _controlledNode;

    /***
     * Called when the node enters the scene tree.
     * Uses deferred call to ensure all nodes are initialized before setting the state.
     */
    public override void _Ready()
    {
        CallDeferred("_readyCalledDefered");
    }

    /***
     * Deferred method called after the node is ready.
     * Initializes the current state to the default state and starts it.
     */
    private void _readyCalledDefered()
    {
        _currentState = _defaultState;
        _startState();
    }

    /***
     * Configures and starts the current state.
     */
    private void _startState()
    {
        _currentState.ControlledNode = _currentState;
        _currentState.StateMachine = this;
        _currentState.Start();
    }

    /***
     * Changes the current state to the specified state.
     * Ensures the previous state is properly ended before switching.
     * @param newState The name of the new state node.
     */
    public void ChangeState(string newState)
    {
        if (_currentState != null)
            _currentState.End();

        GD.Print("Estado cambiado a: " + newState + " desde " + _currentState.Name);
        _currentState = (StateBase)GetNode<Node>(newState);
        _startState();
    }

    /***
     * Gets the current active state.
     */
    public StateBase CurrentState
    {
        get => _currentState;
    }

    #region Automatic Execution Methods

    /***
     * Called every frame. Passes execution to the current state's processing method.
     */
    public override void _Process(double delta)
    {
        _currentState?.OnProcess(delta);
    }

    /***
     * Called during the physics step. Passes execution to the current state's physics processing method.
     */
    public override void _PhysicsProcess(double delta)
    {
        _currentState?.OnPhysicsProcess(delta);
    }

    /***
     * Handles input events and delegates them to the current state.
     */
    public override void _Input(InputEvent @event)
    {
        _currentState?.OnInput(@event);
    }

    /***
     * Handles unhandled input events and delegates them to the current state.
     */
    public override void _UnhandledInput(InputEvent @event)
    {
        _currentState?.OnUnhandledInput(@event);
    }

    /***
     * Handles unhandled key input events and delegates them to the current state.
     */
    public override void _UnhandledKeyInput(InputEvent @event)
    {
        _currentState?.OnUnhandledKeyInput(@event);
    }

    #endregion
}
