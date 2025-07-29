using Godot;
using System;

public partial class VampireDeathState : VampireStateBase
{

	[Export] private AudioStreamPlayer _deathAudio;

	public override void Start()
	{
		base.Start();
		_vampire.AnimationStateMachine.Travel(AnimationNameVampire.Death);
		_deathAudio.Play();
    }
}
