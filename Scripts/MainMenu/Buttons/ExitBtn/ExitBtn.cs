using Godot;
using System;

public partial class ExitBtn : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += _onPressed;
	}

	public void _onPressed()
	{
		GetTree().Quit();
	}

}
