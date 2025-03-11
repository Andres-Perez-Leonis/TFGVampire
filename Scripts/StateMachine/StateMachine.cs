using System;
using Godot;

public partial class StateMachine : Node
{

	[Export]
	private StateBase _defaultState;

	private StateBase _currentState;

	private Node _controlledNode;
	public override void _Ready()
	{
		CallDeferred("_readyCalledDefered");
	}

	private void _readyCalledDefered() {
		_currentState = _defaultState;
		_startState();
	}

	private void _startState() {
		_currentState.ControlledNode = _currentState;
		_currentState.StateMachine = this;
		_currentState.Start();
	}

	public void ChangeState(string newState) {
		if(_currentState != null && _currentState.HasMethod("End")) _currentState.End();
		_currentState = (StateBase)GetNode<Node>(newState);
		GD.Print("Estado cambiado a: " + newState + " desde " + _currentState.Name);
		_startState();
	}

    #region Automatic Ejecution Methods

		public override void _Process(double delta)
		{
			if(_currentState != null)
				_currentState.OnProcess(delta);
		}

		public override void _PhysicsProcess(double delta)
		{
			if(_currentState != null)
				_currentState.OnPhysicsProcess(delta);
		}

		public override void _Input(InputEvent @event)
		{
			if(_currentState != null)
				_currentState.OnInput(@event);
		}

		public override void _UnhandledInput(InputEvent @event)
		{
			if(_currentState != null)
				_currentState.OnUnhandledInput(@event);
		}

		public override void _UnhandledKeyInput(InputEvent @event)
		{
			if(_currentState != null)
				_currentState.OnUnhandledKeyInput(@event);
		}


    #endregion


}
