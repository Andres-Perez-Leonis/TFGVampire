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
			GetTree().ChangeSceneToFile(_levelPath);
    }

    private void InitFirstLevel()
	{
		((Control)Owner).Visible = false;
	}

}
