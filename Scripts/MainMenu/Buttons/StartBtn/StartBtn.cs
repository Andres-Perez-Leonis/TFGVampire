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
		GetTree().ChangeSceneToFile(_levelPath);
	}

}
