using Godot;
using System;

public partial class MainScene : Node2D
{
	[Export] private AudioStreamPlayer _audioConstante;
	[Export] private AudioStreamPlayer _audioPerder;
	[Export] private AudioStreamPlayer _audioTeHanVisto;

	public override void _Ready()
	{
		base._Ready();
		_audioConstante.Play();
	}
}
