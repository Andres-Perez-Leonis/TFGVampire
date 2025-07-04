using Godot;
using System;

public partial class VolverBtn : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += _onPressed;
	}

	private void _onPressed()
	{
		if (Owner is Control menu) menu.Visible = false; 
	}
}
