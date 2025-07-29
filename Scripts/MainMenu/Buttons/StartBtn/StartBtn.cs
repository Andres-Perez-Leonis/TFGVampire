using Godot;
using System;

public partial class StartBtn : Button
{
	private AnimationPlayer _animator;
	[Export] private string _levelPath;

	public override void _Ready()
	{
		_animator = Owner.GetParent().GetNode<AnimationPlayer>("animation/AnimationPlayer");
		_animator.AnimationFinished += InitFirstLevel;
		Pressed += InitFirstLevel;
	}

    private void InitFirstLevel(StringName animName)
    {
		if (MainMenuAnimationName.InitGame.Equals(animName))
		{
			var timer = new Timer
			{
				WaitTime = 0.5,
				OneShot = true,
				Autostart = true
			};

			timer.Timeout += () => GetTree().ChangeSceneToFile(_levelPath);
			AddChild(timer);
		}
    }

    private void InitFirstLevel()
	{
		((Control)Owner).Visible = false;
	}

}
