using Godot;
using System;

public partial class ViewMode : OptionButton
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ItemSelected += _viewModeSelected;
	}

	private void _viewModeSelected(long mode) {
		//if (Owner is ParentNodeMenu c) c.OnSwitchClick();
		switch (mode)
		{
			case 0:
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
				break;
			case 1:
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
				break;
		}
	}
}
