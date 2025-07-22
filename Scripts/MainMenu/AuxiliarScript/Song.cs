using Godot;
using System;

public partial class Song : AudioStreamPlayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Finished += Loop;
	}

	private void Loop()
	{
		Play();
	}
}
