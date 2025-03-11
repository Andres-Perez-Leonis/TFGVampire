using Godot;
using System;
using System.ComponentModel;

public partial class StateBase : Node
{
	private Node _controlledNode;
	private StateMachine _stateMachine;

	public override void _Ready() {
		base._Ready();
		_controlledNode = this.Owner;
	}

	#region Comun Methods of State Machine
		public void Start() {}
		public void End() {}

		public virtual void OnProcess(double delta) {}
		public virtual void OnPhysicsProcess(double delta) {}
		public virtual void OnInput(InputEvent @event) {}
		public virtual void OnUnhandledInput(InputEvent @event) {}
		public virtual void OnUnhandledKeyInput(InputEvent @event) {}
    #endregion

    #region Getters and Setters
    public Node ControlledNode {
			get { return _controlledNode; }
			set { _controlledNode = value; }
		}

		public StateMachine StateMachine {
			get { return _stateMachine; }
			set { _stateMachine = value; }
		}
		
	#endregion

}
