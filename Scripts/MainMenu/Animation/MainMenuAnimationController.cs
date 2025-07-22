using Godot;

public partial class MainMenuAnimationController : AnimationPlayer
{
	[Export] private AudioStreamPlayer _song;
	[Export] private AudioStreamPlayer _laugh;
	public override void _Ready()
	{
		base._Ready();
		GetNode<Button>("../../InitMenu/VBoxContainer/StatBtn").Pressed += InitGame;
		Play(MainMenuAnimationName.WaitingToStart);
		_song.Play();
		AnimationStarted += OnStartAnimation;
	}

	private void OnStartAnimation(StringName name)
	{
		if (MainMenuAnimationName.InitGame.Equals(name))
		{
			_song.Stop();
			_laugh.Play();
		}
	}



	private void InitGame()
	{
		Play(MainMenuAnimationName.InitGame);
	}
}
