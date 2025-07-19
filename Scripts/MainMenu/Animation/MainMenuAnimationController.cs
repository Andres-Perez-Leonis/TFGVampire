using Godot;
using System;

public partial class MainMenuAnimationController : AnimationPlayer
{
	public override void _Ready()
	{
		base._Ready();
		GetNode<Button>("../../InitMenu/VBoxContainer/StatBtn").Pressed += InitGame;
		Play(MainMenuAnimationName.WaitingToStart);
	}

	private void InitGame()
	{
		Play(MainMenuAnimationName.InitGame);
	}
}
