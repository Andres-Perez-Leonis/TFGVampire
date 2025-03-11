using Godot;
using System;

public partial class VampireStateBase : StateBase
{

	protected Vampire _vampire;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		_vampire = (Vampire) ControlledNode;
	}

	
	//private float _gravity = (float) ProjectSettings.GetSetting("physics/2d/default_gravity");

}
