using Godot;
using System;

public partial class StartBtn : Button
{
	[Export] private string _levelPath;

	public override void _Ready()
	{
		Pressed += InitFirstLevel;
	}

	private void InitFirstLevel()
	{
		//if (Owner is ParentNodeMenu c) c.OnButtonClick();
		GetTree().ChangeSceneToFile(_levelPath);
	}

}
