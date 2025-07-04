using Godot;
using System;

public partial class Resolutions : OptionButton
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ItemSelected += _resolutionSelected;
	}

    private void _resolutionSelected(long option)
	{
		switch (option)
		{
			case 0:
				DisplayServer.WindowSetSize(new(1920, 1080));
				break;
			case 1:
				DisplayServer.WindowSetSize(new(1600, 900));
				break;
			case 2:
				DisplayServer.WindowSetSize(new(1280, 720));
				break;
		}
	}
}
